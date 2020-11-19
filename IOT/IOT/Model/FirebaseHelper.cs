using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IOT.Model
{
   public class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://vecihelpapk.firebaseio.com/");

        public async Task<List<Sirena>> GetAllSirena()
        {

            return (await firebase
              .Child("Comunidad")
              .OnceAsync<Sirena>()).Select(item => new Sirena
              {
                  id_Sirena = item.Object.id_Sirena,
                  estado_Sirena = item.Object.id_Sirena,
                  tipo_Sonido = item.Object.id_Sirena

              }).ToList();
        }

        public async Task AddSirena(int id_Sirena, int estado_Sirena,int tipo_Sonido)
        {

            await firebase
              .Child("Comunidad/test")
              .PostAsync(new Sirena() { id_Sirena = id_Sirena, estado_Sirena = estado_Sirena,tipo_Sonido=tipo_Sonido });
        }

        public async Task<Sirena> GetSirena(int id_sirena)
        {
            var allSirenas = await GetAllSirena();
            await firebase
              .Child("Comunidad")
              .OnceAsync<Sirena>();
            return allSirenas.Where(a => a.id_Sirena == id_sirena).FirstOrDefault();
        }

        public async Task UpdateSirena(int id_Sirena, int estado_sirena, int tipo_sonido)
        {
            var toUpdateSirena = (await firebase
              .Child("Comunidad")
              .OnceAsync<Sirena>()).Where(a => a.Object.id_Sirena == id_Sirena).FirstOrDefault();

            await firebase
              .Child("Comunidad")
              .Child(toUpdateSirena.Key)
              .PutAsync(new Sirena() { id_Sirena = id_Sirena, estado_Sirena = estado_sirena,tipo_Sonido=tipo_sonido });
        }

        public async Task DeleteSirena(int id_sirena)
        {
            var toDeleteSirena = (await firebase
              .Child("Comunidad")
              .OnceAsync<Sirena>()).Where(a => a.Object.id_Sirena == id_sirena).FirstOrDefault();
            await firebase.Child("Comunidad").Child(toDeleteSirena.Key).DeleteAsync();

        }
    }
}
