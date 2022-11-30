using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObandoBryan_Examen.Entidades;
using System.Text;

namespace ObandoBryan_Examen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<string>> PostImagen(string imgRoute)
        {
            //------------------------------------------------------------------------------------
            var api = new System.Net.WebClient();
            api.Headers.Add("Content_Type", "application/octec-stream");
            api.Headers.Add("Content_Type", "application/json");
            api.Headers.Add("Ocp-Apim-Subscription-Key", "3eb872f2372242af8c3d473a50e6a71d");
            var qs = "language=es&language=true&model-version=latest";
            var url = "https://eastus.api.cognitive.microsoft.com/vision/v3.2/ocr";

            //byte[] bytes = System.IO.File.ReadAllBytes(cadena);
            //var resp = api.UploadData(url + "?" + qs, "POST", bytes);
            var resp = api.UploadFile(url + "?" + qs, "POST", imgRoute);
            var json = Encoding.UTF8.GetString(resp);
            var text1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ocr_response>(json);
            //------------------------------------------------------------------------------------

            var qsO = "model-version=latest";
            var urlO = "https://eastus.api.cognitive.microsoft.com/vision/v3.2/detect";

            var respO = api.UploadFile(urlO + "?" + qsO, "POST", imgRoute);
            var jsonO = Encoding.UTF8.GetString(respO);
            var textO =  Newtonsoft.Json.JsonConvert.DeserializeObject<do_response>(jsonO);

            return textOcr(text1) + "\n" +textDo(textO);
        }
        private static string textOcr(ocr_response resp)
        {
            var txt = "Texto:\n";
            foreach (var region in resp.regions)
            {
                foreach (var line in region.lines)
                {
                    foreach (var word in line.words)
                    {
                        txt += word.text + " ";
                    }
                    txt += "\n";
                }
                txt += "\n";
            }
            return txt;

        }

        private static string textDo(do_response resp)
        {
            var txt = "Objetos Detectados:\n";
            foreach (var @object in resp.objects)
            {
                txt += "Object: " + @object.Object + "\n";
                parent aux = @object.parent;
                var space = "";
                while ( aux != null)
                {
                    txt += space + "  Parent: " + aux.Object + "\n";
                    aux = aux.Parent;
                    space += "  ";
                }
                space = "";
            }
            return txt;

        }

    }
}
