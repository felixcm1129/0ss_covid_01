using BillingManagement.Business;
using BillingManagement.Models;
using BillingManagement.UI.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BillingManagement.UI.ViewModels
{
    public class CustomerViewModel : BaseViewModel
    {
        readonly CustomersDataService customersDataService = new CustomersDataService();

        private ObservableCollection<Customer> customers;
        private Customer selectedCustomer;

        public ObservableCollection<Customer> Customers
        {
            get => customers;
            private set
            {
                customers = value;
                OnPropertyChanged();
            }
        }

        public Customer SelectedCustomer
        {
            get => selectedCustomer;
            set
            {
                selectedCustomer = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand DeleteCustomerCommand { get; private set; }


        public CustomerViewModel()
        {
            //DeleteCustomerCommand = new DeleteCustomerCommand(DeleteCustomer);

            DeleteCustomerCommand = new RelayCommand(DeleteCustomer, CanDeleteCustomer);
            InitValues();
        }

        private void InitValues()
        {
            Customers = new ObservableCollection<Customer>(customersDataService.GetAll());
            Debug.WriteLine(Customers.Count);
        }

        private void DeleteCustomer(object c)
        {
            Customer customer = c as Customer;

            var currentIndex = Customers.IndexOf(customer);

            if (currentIndex > 0) currentIndex--;

            SelectedCustomer = Customers[currentIndex];

            Customers.Remove(customer);
        }

        private bool CanDeleteCustomer(object c)
        {
            
            if (c == null) return false;

            Customer customer = c as Customer;

            return customer.Invoices.Count == 0;
        }

    }
}
