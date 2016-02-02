## XamarinFormsMvvmExtension
There is extension for Xamarin.Forms.
##What you can do with this?
0. You can use navigation service in ViewModel.
```
    public class MainViewModel : CustomViewModelBase, INavigateAwareViewModel
    {
      public ICommand PushCommand
      {
          get { return new Command(async () => { await NavigateTo<SecondViewModel>("Hello World"); });}
      }
    }
    
    // You can send parameter to this ViewModel trow NavigateTo method
    public class SecondViewModel : CustomViewModelBase, INavigateAwareViewModel
    {
        public void NavigateTo(object parameter)
        {
            MainText = parameter.ToString();
        }
        
        public string MainText
        {
            get { return mainText; }
            set
            {
                mainText = value;
                RaisePropertyChanged(() => MainText);
            }
        }

        public ICommand GoBackCommand
        {
            get { return new Command(async () => { await GoBack(); }); }
        }
    }
```
2. You can get context of ViewModel on code-behind.
3. You can configure your own navifation service and root object.

##TODO:
-Get context of view model on renderer class



