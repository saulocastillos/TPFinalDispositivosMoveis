using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading;

namespace TPFinal
{
    public partial class MainPage : ContentPage
    {

        CancellationTokenSource cts;
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();



            //Get All Books
            var bookList = await App.SQLiteDb.GetItemsAsync();
            if (bookList != null)
            {
                lstBooks.ItemsSource = bookList;
            }
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBookName.Text))
            {
                Book Book = new Book()
                {
                    BookName = txtBookName.Text,
                    BookISBN = txtBookISBN.Text,
                    BookAuthor = txtBookAuthor.Text,
                    BookAuthorEmail = txtBookAuthorEmail.Text
                };

                //Add New Book
                await App.SQLiteDb.SaveItemAsync(Book);
                txtBookId.Text = string.Empty;
                txtBookName.Text = string.Empty;
                txtBookISBN.Text = string.Empty;
                txtBookAuthor.Text = string.Empty;
                txtBookAuthorEmail.Text = string.Empty;
                await DisplayAlert("Success", "Book added Successfully", "OK");
                //Get All Books
                var BookList = await App.SQLiteDb.GetItemsAsync();
                if (BookList != null)
                {
                    lstBooks.ItemsSource = BookList;
                }
            }
            else
            {
                await DisplayAlert("Required", "Book name is required, please enter!", "OK");
            }
        }

        private async void BtnRead_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBookId.Text))
            {
                //Get Book
                var Book = await App.SQLiteDb.GetItemAsync(Convert.ToInt32(txtBookId.Text));
                if (Book != null)
                {
                    txtBookName.Text = Book.BookName;
                    txtBookISBN.Text = Book.BookISBN;
                    txtBookAuthor.Text = Book.BookAuthor;
                    txtBookAuthorEmail.Text = Book.BookAuthorEmail;
                    await DisplayAlert("Success", "Book Name: " + Book.BookName, "OK");
                }
            }
            else
            {
                await DisplayAlert("Required", "Please Enter BookID", "OK");
            }
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBookId.Text))
            {
                Book Book = new Book()
                {
                    BookId = Convert.ToInt32(txtBookId.Text),
                    BookName = txtBookName.Text,
                    BookISBN = txtBookISBN.Text,
                    BookAuthor = txtBookAuthor.Text,
                    BookAuthorEmail = txtBookAuthorEmail.Text
                };

                //Update Book
                await App.SQLiteDb.SaveItemAsync(Book);

                txtBookId.Text = string.Empty;
                txtBookName.Text = string.Empty;
                txtBookISBN.Text = string.Empty;
                txtBookAuthor.Text = string.Empty;
                txtBookAuthorEmail.Text = string.Empty;
                await DisplayAlert("Success", "Book Updated Successfully", "OK");
                //Get All Books
                var BookList = await App.SQLiteDb.GetItemsAsync();
                if (BookList != null)
                {
                    lstBooks.ItemsSource = BookList;
                }

            }
            else
            {
                await DisplayAlert("Required", "Please Enter BookID", "OK");
            }
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBookId.Text))
            {
                //Get Book
                var Book = await App.SQLiteDb.GetItemAsync(Convert.ToInt32(txtBookId.Text));
                if (Book != null)
                {
                    //Delete Book
                    await App.SQLiteDb.DeleteItemAsync(Book);
                    txtBookId.Text = string.Empty;
                    txtBookName.Text = string.Empty;
                    txtBookISBN.Text = string.Empty;
                    txtBookAuthor.Text = string.Empty;
                    txtBookAuthorEmail.Text = string.Empty;
                    await DisplayAlert("Success", "Book Deleted", "OK");

                    //Get All Books
                    var BookList = await App.SQLiteDb.GetItemsAsync();
                    if (BookList != null)
                    {
                        lstBooks.ItemsSource = BookList;
                    }
                }
            }
            else
            {
                await DisplayAlert("Required", "Please Enter BookID", "OK");
            }
        }

        private async void BtnLocalizacao_Clicked(object sender, EventArgs e)
        {
            try
            {
                var lastLocation = await Geolocation.GetLastKnownLocationAsync();
                
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                
                var newLocation = await Geolocation.GetLocationAsync(request, cts.Token);

                if (lastLocation != null && newLocation != null)
                {
                    await DisplayAlert("Informçaões do GPS:",
                        $"Última localização:\n\n" +
                        $"Latitude: {lastLocation.Latitude}\n" +
                        $"Longitude: {lastLocation.Longitude}\n" +
                        $"Altitude: { lastLocation.Altitude}\n\n" +
                        $"Localização atualizada:\n\n" +
                        $"Latitude: { newLocation.Latitude}\n" +
                        $"Longitude: { newLocation.Longitude}\n" +
                        $"Altitude: {newLocation.Altitude}\n", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro no GPS:", $"Erro ao mostrar localização do dispositivo", "Ok");
            }
        }


    }
}
