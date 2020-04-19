using BillingManagement.Models;
using BillingManagement.UI.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillingManagement.UI.ViewModels
{
    class MainViewModel : BaseViewModel
    {
		private BaseViewModel _vm;

		public BaseViewModel VM
		{
			get { return _vm; }
			set {
				_vm = value;
				OnPropertyChanged();
			}
		}

		CustomerViewModel customerViewModel;
		InvoiceViewModel invoiceViewModel;

		public RelayCommand ChangeViewCommand { get; set; }

		public RelayCommand DisplayInvoiceCommand { get; private set; }

		public RelayCommand DisplayCustomerCommand { get; private set; }

		public RelayCommand NewCustomerCommand { get; private set; }

		public RelayCommand NewInvoiceCommand { get; private set; }

		public MainViewModel()
		{
			ChangeViewCommand = new RelayCommand(ChangeView);

			NewCustomerCommand = new RelayCommand(NewCustomer);

			DisplayInvoiceCommand = new RelayCommand(DisplayInvoice);

			NewInvoiceCommand = new RelayCommand(NewInvoice, CanExecuteNewInvoice);

			DisplayCustomerCommand = new RelayCommand(DisplayCustomer, CanExecuteDisplayCustomer);

			customerViewModel = new CustomerViewModel();
			invoiceViewModel = new InvoiceViewModel(customerViewModel.Customers);

			VM = customerViewModel;

		}

		private void ChangeView(object vm)
		{
			switch (vm as string)
			{
				case "customers":
					VM = customerViewModel;
					break;
				case "invoices":
					VM = invoiceViewModel;
					break;
			}
		}

		private void NewCustomer(object c)
		{
			Customer customer = new Customer();

			customerViewModel.Customers.Add(customer);
			customerViewModel.SelectedCustomer = customer;

			VM = customerViewModel;
		}

		private void DisplayInvoice(object i)
		{
			Invoice invoice = i as Invoice;

			invoiceViewModel.SelectedInvoice = invoice;
			VM = invoiceViewModel;
		}

		private void NewInvoice(object c)
		{
			var customer = c as Customer;
			var invoice = new Invoice(customer);

			customer.Invoices.Add(invoice);

			DisplayInvoice(invoice);

		}

		private bool CanExecuteNewInvoice(object c)
		{
			return c == null ? false : true;
		}

		private void DisplayCustomer(object c)
		{
			var customer = c as Customer;

			customerViewModel.SelectedCustomer = customer;
			VM = customerViewModel;
		}

		private bool CanExecuteDisplayCustomer(object c)
		{
			return c == null ? false : true;
		}

	}
}
