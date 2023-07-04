using SQLite;
using TravelRecordApp.Models;
using TravelRecordApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PostDetailsPage : ContentPage
	{
		public PostDetailsPage (Post post)
		{
			InitializeComponent ();

			(Resources["viewModel"] as PostDetailsVM).SelectedPost = post;

            experienceEntry.Text = post.Experience;
		}
    }
}