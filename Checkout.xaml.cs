﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentDBTodo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Stripe;

namespace DocumentDBTodo
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Checkout : ContentPage
	{
        public double Total { get; set; }
		public Checkout (double Total)
		{
			InitializeComponent ();
            this.Total = Total;
		}
        public void CompleteOrder(Object sender, EventArgs e)
        {
            CardInfo newCard = new CardInfo();
            newCard.CardNumber = CardNumber.Text;
            newCard.CardExpMonth = Int32.Parse(ExpMonth.Text);
            newCard.CardExpyear = Int32.Parse(ExpYear.Text);
            newCard.CardCVV = CVV.Text;
            var StripeToken = CreateToken(newCard);
            StripeProcess(StripeToken);
        }
        public string CreateToken(CardInfo card)
        {
            StripeConfiguration.SetApiKey("pk_test_VQtoByzEBwFhM1EKXuqI4ueC");

            var tokenOptions = new StripeTokenCreateOptions()
            {
                Card = new StripeCreditCardOptions()
                {
                    Number = card.CardNumber,
                    ExpirationYear = card.CardExpyear,
                    ExpirationMonth = card.CardExpMonth,
                    Cvc = card.CardCVV
                }
            };

            var tokenService = new StripeTokenService();
            StripeToken stripeToken = tokenService.Create(tokenOptions);

            return stripeToken.Id; // This is the token
        }
        public void StripeProcess(string Token)
        {
            var charge = new StripeChargeCreateOptions
            {
                Amount = Convert.ToInt32(Total * 100), // In cents, not dollars, times by 100 to convert
                Currency = "usd", // or the currency you are dealing with
                Description = "StadiYUM Order",
                SourceTokenOrExistingSourceId = Token
            };

            var service = new StripeChargeService("pk_test_VQtoByzEBwFhM1EKXuqI4ueC");

            try
            {
                var response = service.Create(charge);
                if (response.Paid)
                {
                    DisplayAlert("Order Confirmed", "Go to Orders to view status", "OK");
                }
                // Record or do something with the charge information
            }
            catch (StripeException ex)
            {
                StripeError stripeError = ex.StripeError;
                DisplayAlert("Error", stripeError.Error, "OK");

                // Handle error
            }

            // Ideally you would put in additional information, but you can just return true or false for the moment.
            
        }
	}
}