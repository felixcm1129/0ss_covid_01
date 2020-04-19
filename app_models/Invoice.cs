using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace BillingManagement.Models
{
    public class Invoice : INotifyPropertyChanged
    {
        static int nextId;

        public int InvoiceId { get; private set; }

        public DateTime CreationDateTime { get; private set; }

        private Customer _customer;

        public Customer Customer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                OnPropertyChanged();
            }
        }

        private double subTotal;

        public double SubTotal
        {
            get { return subTotal; }
            set
            {
                subTotal = value;
                OnPropertyChanged();

                /// Seulement pour les besoins pédagogiques
                /// Logiquement cela devrait être inversé avec un Attribut
                /// https://stackoverflow.com/a/32892322/503842
                OnPropertyChanged(nameof(FedTax));
                OnPropertyChanged(nameof(ProvTax));
                OnPropertyChanged(nameof(Total));
            }
        }

        public double FedTax => SubTotal * 0.05;
        public double ProvTax => SubTotal * 0.09975;

        public double Total => SubTotal + FedTax + ProvTax;

        public string Info => $"{CreationDateTime} : {Total:C2}";

        public Invoice()
        {
            // Incremente le ID
            InvoiceId = Interlocked.Increment(ref nextId);

            CreationDateTime = DateTime.Now;
        }

        public Invoice(Customer customer)
        {
            // Incremente le ID
            InvoiceId = Interlocked.Increment(ref nextId);

            CreationDateTime = DateTime.Now;
            Customer = customer;
        }

        #region Notifying stuffs
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
