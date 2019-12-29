using System.Threading.Tasks;
using Movies.ViewModels.Base;
using Movies.Models.Movie;
using Movies.Services.People;
using Movies.Models.People;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;
using Movies.Services.Navigation;
using Movies.Models;

namespace Movies.ViewModels
{
    public class PeopleViewModel : ViewModelBase
    {
        private Person _person;
        private ObservableCollection<Profile> _images;

        private IPeopleService _peopleService;
        private INavigationService _navigationService;

        public PeopleViewModel(
            IPeopleService peopleService,
            INavigationService navigationService)
        {
            _peopleService = peopleService;
            _navigationService = navigationService;
        }

        public Person Person
        {
            get { return _person; }
            set
            {
                _person = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Profile> Images
        {
            get { return _images; }
            set
            {
                _images = value;
                OnPropertyChanged();
            }
        }

        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData is MovieCastMember)
            {
                IsBusy = true;

                var movieCastMember = (MovieCastMember)navigationData;
                Person = await _peopleService.FindByIdAsync(movieCastMember.PersonId);

                var images = await _peopleService.GetImagesAsync(movieCastMember.PersonId);
                Images = new ObservableCollection<Profile>(images.profiles.Take(10));
                MessagingCenter.Send(this, AppSettings.ImagesMessage, Images as object);

                IsBusy = false;
            }
        }
    }
}