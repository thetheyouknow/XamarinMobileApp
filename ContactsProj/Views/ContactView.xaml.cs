using ContactsProj.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactsProj.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactView : ContentPage
	{
		public ContactView(ContactViewModel viewmodel)
		{
			InitializeComponent();
			viewmodel.Navigation = Navigation;
			BindingContext = viewmodel;
		}
	}
}
