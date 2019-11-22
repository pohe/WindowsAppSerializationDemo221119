using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.UI.Popups;
using WindowsAppSerializationDemo221119.Model;
using Newtonsoft.Json;

namespace WindowsAppSerializationDemo221119.Persistency
{
    class PersistenceFacade
    {
        private static string jsonFileName = "PersonsAsJson1.dat";
        private static string xmlFileName = "PersonsAsXml1.dat";

        public static async void SavePersonsAsJsonAsync(ObservableCollection<Person> persons)
        {
            string personsJsonString = JsonConvert.SerializeObject(persons);
            SerializePersonsFileAsync(personsJsonString, jsonFileName);
        }

        public static async Task<List<Person>> LoadPersonsFromJsonAsync()
        {
            string personsJsonString = await DeSerializePersonsFileAsync(jsonFileName);
            if (personsJsonString != null)
                return (List<Person>)JsonConvert.DeserializeObject(personsJsonString, typeof(List<Person>));
            return null;
        }

        public static async void SavePersonsAsXmlAsync(ObservableCollection<Person> persons)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(persons.GetType());
            StringWriter textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, persons);
            SerializePersonsFileAsync(textWriter.ToString(), xmlFileName);
        }

        public static async Task<ObservableCollection<Person>> LoadPersonsFromXmlAsync()
        {
            string personsXmlString = await DeSerializePersonsFileAsync(xmlFileName);
            if (personsXmlString != null)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<Person>));
                return (ObservableCollection<Person>)xmlSerializer.Deserialize(new StringReader(personsXmlString));
            }
            return null;
        }


        public static async void SerializePersonsFileAsync(string PersonsString, string fileName)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(localFile, PersonsString);
        }

        public static async Task<string> DeSerializePersonsFileAsync(String fileName)
        {
            try
            {
                StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return await FileIO.ReadTextAsync(localFile);
            }
            catch (FileNotFoundException ex)
            {

                MessageDialogHelper.Show("Loading for the first time? Try Adding and Save some Persons before you are trying to Load Persons!", "File not found!");
                return null;
            }
        }

        private class MessageDialogHelper
        {
            public static async void Show(string content, string title)
            {
                MessageDialog messageDialog = new MessageDialog(content, title);
                await messageDialog.ShowAsync();
            }
        }



    }

}
