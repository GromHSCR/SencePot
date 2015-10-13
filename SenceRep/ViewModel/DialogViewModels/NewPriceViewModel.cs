namespace RedKassa.Promoter.ViewModel.DialogViewModels
{
	class NewPriceViewModel
	{
		public NewPriceViewModel(string labelText, decimal price)
		{
			LabelText = labelText;
			Price = price;
		}

		public decimal Price { get; set; }

		public string LabelText { get; set; }
	}
}
