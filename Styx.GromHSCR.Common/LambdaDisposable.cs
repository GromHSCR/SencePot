using System;

namespace Styx.GromHSCR.Common
{
	/// <summary>
	/// Guarantees automatic execution of delegate functions
	/// when an instance of this class is constructed and/or
	/// when it is disposed.
	/// </summary>
	/// <remarks>
	/// The <b>LambdaDisposable</b> class is used in cases where
	/// you would normally use a try/finally to ensure that
	/// your cleanup code is executed, regardless of
	/// whether an exception was thrown or not.  With the
	/// <b>LambdaDisposable</b> class, instead of all of the
	/// try/catch lines which can make code less readable,
	/// you can leverage the <see langword="using"/>
	/// statement, instantiating an instance of this class,
	/// and pass either just the cleanup code or both the
	/// constructor code and cleanup code to execute.
	/// </remarks>
	public class LambdaDisposable : IDisposable
	{
		private bool _disposed;
		private readonly Action _executeOnDispose;

		/// <summary>
		/// Constructs an <see cref="LambdaDisposable"/> object,
		/// immediately executing a delegate function, and
		/// the guaranteeing the execution of a second
		/// deletate function when disposed.
		/// </summary>
		/// <param name="executeOnConstruct">
		/// The delegate function to execute during
		/// construction.
		/// </param>
		/// <param name="executeOnDispose">
		/// The delegate function to execute when disposed.
		/// </param>
		public LambdaDisposable(Action executeOnConstruct,
						   Action executeOnDispose)
		{
			if (executeOnConstruct != null)
			{
				executeOnConstruct();
			}
			this._executeOnDispose = executeOnDispose;
		}

		/// <summary>
		/// Constructs an <see cref="LambdaDisposable"/> object,
		/// guaranteeing the execution of a provided delegate
		/// function when disposed.
		/// </summary>
		/// <param name="executeOnDispose">
		/// The delegate function to execute when disposed.
		/// </param>
		public LambdaDisposable(Action executeOnDispose)
		{
			this._executeOnDispose = executeOnDispose;
		}

		#region IDisposable Members
		/// <summary>
		/// Disposes the <see cref="LambdaDisposable"/> object,
		/// executing the delegate function provided in the
		/// constructor.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		//
		// Internal implementation of the Dispose() method.
		// See the MSDN documentation on the IDisposable
		// interface for a detailed explanation of this
		// pattern.
		//
		private void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				//
				// When disposing is true, release all
				// managed resources.
				//
				if (disposing)
				{
					if (null != this._executeOnDispose)
					{
						this._executeOnDispose();
					}
				}
				_disposed = true;
			}
		}
		#endregion
	}
}
