using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Calistenia_Calculadora.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace Calistenia_Calculadora.Controllers;

public class UsuariosControllers : Controller
{
    public string UriBase = "http://Calistenia-Calculadora.somee.com/Usuarios/";


    [HttpGet]
    public ActionResult Index()
    {
        return View("CadastrarUsuarios");
    }

    [HttpPost]
    public async Task<AcceptedResult> RegistrarAsync(UsuarioViewModel u)
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            string uriComplementar = "Registar";

            var content = new StringContent(JsonConvert.SerializeObject(u));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(UriBase + uriComplementar, content);

            string serialized = await response.Content.ReadAsStringAsync();

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                TempData["Mensagem"] = 
                    string.Format($"Usuarios{u.Username} registrado com sucesso! Faça o login para acessar.");
                return View("AutenticarUsuario");
            }else
            {
                throw new System.Exception(serialized);
            }

        }
        catch (System.Exception ex)
        {
            TempData["MensagemErro"] = ex.Message;
            return RedirectToAction("Index");
        }
    }

    [HttpGet]
    public ActionResult IndexLogin()
    {
        return View("AutenicarUsuario");
    }

    [HttpPost]
    public async Task<ActionResult> AutenticarAsync(UsuarioViewModel u)
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            string uriComplementar = "Autenticar";
            var content = new StringContent(JsonConvert.SerializeObject(u));
            content.Headers.ContentType = new MediaTypeHeaderValue("Application/json");
            HttpResponseMessage response = await httpClient.PostAsync(UriBase + uriComplementar,content);

            string serialized = await response.Content.ReadAsStringAsync();

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                UsuarioViewModel uLogado = JsonConvert.DeserializeObject<UsuarioViewModel>(serialized);
                HttpContext.Session.SetString("SessionTokenUsuario", uLogado.Token);
                TempData["Mensagem"] = string.Format($"Bem-vindo{uLogado.Username}");
                return RedirectToAction("Index", "Personagens");
                
            }
            else{
                throw new System.Exception(serialized);
            }
        }
        catch(System.Exception ex)
        {
            TempData["MensagemErro"] = ex.Message;
            return IndexLogin();
        }
    }
}
