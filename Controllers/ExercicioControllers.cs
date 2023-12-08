using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Calistenia_Calculadora.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;

namespace Calistenia_Calculadora.Controllers
{
    public class PersonagensControllers : Controller
    {
         public string uriBase = "http://Calistenia-Calculadora.somee.com/Exercicios/";

    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {

        try
        {
            string uriComplementar ="GetAll";
            HttpClient httpClient = new HttpClient();
            string token = HttpContext.Session.GetString("SessionTokenUsuario");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await httpClient.GetAsync(uriBase + uriComplementar);
            string serialized = await response.Content.ReadAsStringAsync();

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                List<ExerciciosViewModel> listaPersonagens = await Task.Run(()=>
                    JsonConvert.DeserializeObject<List<ExercicioViewModel>>(serialized);

            }
        }
        catch (System.Exception ex)
        {
            TempDataAttribute["MensagemErro"] = ex.Message;
            return RedirectToAction("Index");
        }
    }
}