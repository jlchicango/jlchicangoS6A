using jlchicangoS6A.Models;
using System.Net;
namespace jlchicangoS6A.Views;

public partial class vActualizarElim : ContentPage
{
	public vActualizarElim(Estudiante datos)
	{
		InitializeComponent();
        txtCodigo.Text = datos.codigo.ToString();
        txtNombre.Text = datos.nombre.ToString();
        txtApellido.Text = datos.apellido.ToString();
        txtEdad.Text = datos.edad.ToString();
    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        WebClient client = new WebClient();
        var parametros = new System.Collections.Specialized.NameValueCollection();
        parametros.Add("nombre", txtNombre.Text);
        parametros.Add("apellido", txtApellido.Text);
        parametros.Add("edad", txtEdad.Text);
        string urlput = "http://localhost/uisrael/estudiante.php?codigo=" + txtCodigo.Text + "&nombre=" + txtNombre.Text + "&apellido=" + txtApellido.Text + "&edad=" + txtEdad.Text;
        client.UploadValues(urlput, "PUT", parametros);
        await Navigation.PushAsync(new vEstudiante());
        await DisplayAlert("Exito", "Actualizado correctamente", "Aceptar");
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        var result = await DisplayAlert("Eliminar", "�Est� seguri que desea eliminar este elemento?", "Eliminar", "Cancelar");
        if (result)
        {
            try
            {
                WebClient client = new WebClient();
                string urldelete = "http://localhost/uisrael/estudiante.php?codigo=" + txtCodigo.Text;

                // Realizar la petici�n DELETE
                client.UploadValues(urldelete, "DELETE", new System.Collections.Specialized.NameValueCollection());

                await DisplayAlert("�xito", "Eliminado correctamente", "Aceptar");

                // Navegar de vuelta a la vista principal
                await Navigation.PushAsync(new vEstudiante());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Ocurri� un error al eliminar: " + ex.Message, "Aceptar");
            }
            await DisplayAlert("Eliminar", "Eliminado correctamente", "Aceptar");
        }
    }
}