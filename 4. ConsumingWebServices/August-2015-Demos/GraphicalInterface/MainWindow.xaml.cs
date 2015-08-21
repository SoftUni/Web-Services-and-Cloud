namespace GraphicalInterface
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string GetAllPostsEndpoint = "http://localhost:64411/api/posts";

        public MainWindow()
        {
            InitializeComponent();

            this.button.Click += Button_Click;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var task = this.PrintPosts();
        }

        private async Task PrintPosts()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(GetAllPostsEndpoint);
            
            MessageBox.Show(response.ToString());
        }
    }
}
