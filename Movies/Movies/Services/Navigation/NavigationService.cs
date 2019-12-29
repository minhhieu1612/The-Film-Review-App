﻿using Movies.ViewModels;
using Movies.ViewModels.Base;
using Movies.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Movies.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        protected readonly Dictionary<Type, Type> _mappings;

        protected Application CurrentApplication
        {
            get { return Application.Current; }
        }

        public NavigationService()
        {
            _mappings = new Dictionary<Type, Type>();

            CreatePageViewModelMappings();
        }

        public Task InitializeAsync()
        {
            return NavigateToAsync<SplashViewModel>();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public Task NavigateToAsync(Type viewModelType, object parameter)
        {
            return InternalNavigateToAsync(viewModelType, parameter);
        }

        public async Task NavigateBackAsync()
        {
            if (CurrentApplication.MainPage is MainView)
            {
                var mainPage = CurrentApplication.MainPage as MainView;
                await mainPage.Detail.Navigation.PopAsync();
            }
            else if (CurrentApplication.MainPage != null)
            {
                await CurrentApplication.MainPage.Navigation.PopAsync();
            }
        }

        public virtual Task RemoveLastFromBackStackAsync()
        {
            var mainPage = CurrentApplication.MainPage as MainView;

            if (mainPage != null)
            {
                mainPage.Detail.Navigation.RemovePage(
                    mainPage.Detail.Navigation.NavigationStack[mainPage.Detail.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreateAndBindPage(viewModelType, parameter);
            
            if (page is SplashView)
            {
                CurrentApplication.MainPage = new CustomNavigationPage(page);
            }
            else if (page is MainView)
            {
                CurrentApplication.MainPage = page;
            }
            else if (CurrentApplication.MainPage is MainView) 
            {
                var mainPage = CurrentApplication.MainPage as MainView;
                var navigationPage = mainPage.Detail as CustomNavigationPage;

                if (navigationPage != null)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    navigationPage = new CustomNavigationPage(page);
                    mainPage.Detail = navigationPage;
                }

                mainPage.IsPresented = false;
            }
            else
            {
                var navigationPage = CurrentApplication.MainPage as CustomNavigationPage;

                if (navigationPage != null)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    CurrentApplication.MainPage = new CustomNavigationPage(page);
                }
            }

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return _mappings[viewModelType];
        }

        protected Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            ViewModelBase viewModel = Locator.Instance.Resolve(viewModelType) as ViewModelBase;
            page.BindingContext = viewModel;

            return page;
        }

        private void CreatePageViewModelMappings()
        {
            _mappings.Add(typeof(DetailViewModel), typeof(DetailView));
            
            _mappings.Add(typeof(HomeViewModel), typeof(HomeView));
            _mappings.Add(typeof(MainViewModel), typeof(MainView));
            _mappings.Add(typeof(MoviesViewModel), typeof(MoviesView));
            _mappings.Add(typeof(HomepageViewModel), typeof(HomepageView));
            _mappings.Add(typeof(PeopleViewModel), typeof(PeopleView));
            _mappings.Add(typeof(SplashViewModel), typeof(SplashView));
            _mappings.Add(typeof(UpcomingViewModel), typeof(UpcomingView));
            _mappings.Add(typeof(ActionViewModel), typeof(ActionView));
            _mappings.Add(typeof(AnimationViewModel), typeof(AnimationView));
            _mappings.Add(typeof(AdventureViewModel), typeof(AdventureView));
            _mappings.Add(typeof(ComedyViewModel), typeof(ComedyView));
            _mappings.Add(typeof(HorrorViewModel), typeof(HorrorView));
            _mappings.Add(typeof(SearchViewModel), typeof(SearchView));
            _mappings.Add(typeof(SearchResultsViewModel), typeof(SearchResultsView));
        }
    }
}