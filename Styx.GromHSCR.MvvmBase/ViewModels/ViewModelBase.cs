using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Styx.GromHSCR.MvvmBase.Attributes;
using Styx.GromHSCR.MvvmBase.Common;
using Styx.GromHSCR.MvvmBase.Initializations;
using Styx.GromHSCR.MvvmBase.Modifications;
using Styx.GromHSCR.MvvmBase.Validations;

namespace Styx.GromHSCR.MvvmBase.ViewModels
{
	public abstract class ViewModelBase : GalaSoft.MvvmLight.ViewModelBase,
		IViewModel,
		IModification,
		IDataErrorInfo,
		IValidation,
		IInitialization
	{
		#region abstract ctors

		protected ViewModelBase(IMessenger messenger)
			: base(messenger)
		{
			StatusViewModel = StatusViewModel.Unchanged;
			ViewModelProperties = GetType().GetProperties().ToDictionary(p => p.Name);
			InitViewModelBase();
		}

		private void InitViewModelBase()
		{
		}

		protected ViewModelBase()
			: this(null)
		{
		}

		#endregion

		#region private members

		private bool _isModified;
		private bool _affectIsModified;
		private Dictionary<string, AffectIsModifiedAttribute> _affectIsModifiedProperties;
		private Dictionary<string, string> _validationErrors;
		private Dictionary<string, List<ValidationAttribute>> _propertiesValidationAttributes;

		private bool AffectIsModifiedPropertyByName(string propertyName)
		{
			if (_affectIsModifiedProperties == null)
			{
				var mainAffectIsModifiedAttribute = (AffectIsModifiedAttribute)Attribute.GetCustomAttribute(this.GetType(), typeof(AffectIsModifiedAttribute), true);

				_affectIsModified = mainAffectIsModifiedAttribute != null && mainAffectIsModifiedAttribute.AffectIsModified;

				_affectIsModifiedProperties = ViewModelProperties.Values.Select(propertyInfo =>
				{
					var affectIsModifiedAttribute =
						(AffectIsModifiedAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(AffectIsModifiedAttribute), true);

					return affectIsModifiedAttribute == null
							   ? null
							   : new { propertyInfo.Name, AffectIsModifiedAttribute = affectIsModifiedAttribute };
				}).Where(p => p != null).ToDictionary(p => p.Name, p => p.AffectIsModifiedAttribute);
			}

			return _affectIsModified &&
				   (_affectIsModifiedProperties.ContainsKey(propertyName) &&
					_affectIsModifiedProperties[propertyName].AffectIsModified);
		}

		private void SetIsModified(string propertyName)
		{
			if (!IsModified && AffectIsModifiedPropertyByName(propertyName))
				IsModified = true;
		}

		private void OnIsModifiedChanged()
		{
			var handler = IsModifiedChanged;
			if (handler != null) handler(this, EventArgs.Empty);
		}

		private void UpdateValidationErrors(string propertyName)
		{
			if (propertyName == "IsValid" || propertyName == "Error") return;

			var errorMessage = GetPropertyErrorMessage(propertyName);

			if (errorMessage != null)
				ValidationErrors[propertyName] = errorMessage;
			else
				if (ValidationErrors.ContainsKey(propertyName))
					ValidationErrors.Remove(propertyName);

			RaisePropertyChanged("IsValid");
			RaisePropertyChanged("Error");
		}

		#endregion

		#region protected members

		protected virtual void InitializationCommands()
		{

		}

		protected virtual void Initialization()
		{
			InitializationCommands();
		}

		protected ICommand CreateCommand<T>(Action<T> execute, Func<T, bool> canExecute)
		{
			if (execute == null) throw new ArgumentNullException("execute");
			if (canExecute == null) throw new ArgumentNullException("canExecute");

			return new RelayCommand<T>(execute, canExecute);
		}

		protected ICommand CreateCommand<T>(Action<T> execute)
		{
			if (execute == null) throw new ArgumentNullException("execute");

			return new RelayCommand<T>(execute);
		}

		protected ICommand CreateCommand(Action execute, Func<bool> canExecute)
		{
			if (execute == null) throw new ArgumentNullException("execute");
			if (canExecute == null) throw new ArgumentNullException("canExecute");

			return new RelayCommand(execute, canExecute);
		}

		protected ICommand CreateCommand(Action execute)
		{
			if (execute == null) throw new ArgumentNullException("execute");

			return new RelayCommand(execute);
		}

		protected Dictionary<string, List<ValidationAttribute>> PropertiesValidationAttributes
		{
			get
			{
				return _propertiesValidationAttributes ??
					   (_propertiesValidationAttributes = ViewModelProperties.Select(p =>
					   {
						   var customAttributes =
							   Attribute.GetCustomAttributes(p.Value, typeof(ValidationAttribute), true).Cast<ValidationAttribute>().
								   ToList();

						   return customAttributes.Count <= 0 ? null : new { p.Key, customAttributes };
					   }).Where(p => p != null).ToDictionary(p => p.Key, p => p.customAttributes));
			}
		}

		protected virtual string GetValidationError()
		{
			return ValidationErrors.Count > 0
					   ? String.Join(Environment.NewLine, ValidationErrors.Values)
					   : null;
		}

		protected virtual string GetValidationError(string propertyName)
		{
			return ValidationErrors.ContainsKey(propertyName) ? ValidationErrors[propertyName] : null;
		}

		protected virtual string GetPropertyErrorMessage(string propertyName)
		{
			if (!PropertiesValidationAttributes.ContainsKey(propertyName)) return null;

			var propertieValidationAttributes = PropertiesValidationAttributes[propertyName];

			var value = ViewModelProperties[propertyName].GetValue(this,
																	BindingFlags.GetField | BindingFlags.GetProperty |
																	BindingFlags.Instance | BindingFlags.Static, null, null, null);
			var validationContext = new ValidationContext(this) { MemberName = propertyName };
			var errorMessagesWithValidationContext = propertieValidationAttributes.Where(p => p.RequiresValidationContext)
				.Select(p => p.GetValidationResult(value, validationContext))
				.Where(x => x != null && !string.IsNullOrWhiteSpace(x.ErrorMessage))
				.Select(p => p.ErrorMessage).ToList();

			var errorMessages = propertieValidationAttributes.Where(p => !p.IsValid(value))
				.Select(p => p.FormatErrorMessage(propertyName))
				.Union(errorMessagesWithValidationContext).ToList();

			return errorMessages.Count > 0 ? String.Join(Environment.NewLine, errorMessages) : null;
		}

		#region override

		protected override void RaisePropertyChanged(string propertyName)
		{
			UpdateValidationErrors(propertyName);
			base.RaisePropertyChanged(propertyName);
			SetIsModified(propertyName);
		}

		protected override void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
		{
			var propertyName = GetPropertyName(propertyExpression);
			RaisePropertyChanged(propertyName);
		}

		#endregion

		#endregion

		#region public members

		public Dictionary<string, PropertyInfo> ViewModelProperties { get; set; }

		#endregion

		#region public members

		#region IModification implemented

		public event EventHandler<EventArgs> IsModifiedChanged;

		public bool IsModified
		{
			get { return _isModified; }
			set
			{
				if (_isModified == value) return;

				RaisePropertyChanging("IsModified");
				_isModified = value;
				RaisePropertyChanged("IsModified");
				OnIsModifiedChanged();
				if (StatusViewModel == StatusViewModel.Unchanged)
					StatusViewModel = StatusViewModel.Change;
			}
		}

		public StatusViewModel StatusViewModel { get; set; }

		#endregion

		#region IDataErrorInfo

		public string this[string columnName]
		{
			get { return GetValidationError(columnName); }
		}

		public string Error { get { return GetValidationError(); } }

		#endregion

		#region IValidation

		public Dictionary<string, string> ValidationErrors
		{
			get
			{
				return _validationErrors ?? (_validationErrors = ViewModelProperties.Select(propertyInfo =>
				{
					var message = GetPropertyErrorMessage(propertyInfo.Key);
					return message == null ? null : new { PropertyName = propertyInfo.Key, Message = message };
				}).Where(p => p != null).ToDictionary(p => p.PropertyName, p => p.Message));
			}
		}

		public virtual bool IsValid
		{
			get { return ValidationErrors.Count <= 0; }
		}

		#endregion

		#region IInitialization

		void IInitialization.Initialization()
		{
			Initialization();
		}

		#endregion

		#endregion
	}
}
