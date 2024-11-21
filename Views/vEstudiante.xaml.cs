using Newtonsoft.Json;
using jlchicangoS6A.Models;
using System.Collections.ObjectModel;
namespace jlchicangoS6A.Views;

public partial class vEstudiante : ContentPage
{
    private const string Url = "http://localhost/uisrael/estudiante.php";
    private readonly HttpClient cliente = new HttpClient();
    private ObservableCollection<Estudiante> estud;
    public vEstudiante()
	{
        InitializeComponent();
        mostrar();
    }

    public async void mostrar()
    {
        var content = await cliente.GetStringAsync(Url);
        List<Estudiante> mostrarEst = JsonConvert.DeserializeObject<List<Estudiante>>(content);
        estud = new ObservableCollection<Estudiante>(mostrarEst);
        //lvEstudiantes.ItemsSource = estud;
        gvEstudiantes.ItemsSource = estud;

    }

    private void btnIsertar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new vInsertar());
    }

    private void lvEstudiantes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var objEstudiante = (Estudiante)e.SelectedItem;
        Navigation.PushAsync(new vActualizarElim(objEstudiante));
    }

    private void gvEstudiantes_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //var objEstudiante = (Estudiante)e.CurrentSelection;
        var estudianteSeleccionado = e.CurrentSelection.FirstOrDefault() as Estudiante;
        if (estudianteSeleccionado != null)
        {
            Navigation.PushAsync(new vActualizarElim(estudianteSeleccionado));
        }
        // Limpiar la selección después de manejarla
    ((CollectionView)sender).SelectedItem = null;
    }
}