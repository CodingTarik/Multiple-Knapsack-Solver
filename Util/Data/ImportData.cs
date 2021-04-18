using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace BWI_Hardwareverteilung.Util.Data
{
    /// <summary>
    /// Diese Klasse kann abgespeicherte und serialisierte XML Dateien vom Typ serilzableDataObject lesen und somit abgespeicherte Daten wieder deserialisieren
    /// </summary>
    class ImportData
    {
        /// <summary>
        /// Diese Methode öffnet einen FileDialog zum Öffnen der gespeicherte XML Datei, deserialisiert diese und gibt ein SerializableDataObject zurück
        /// </summary>
        /// <returns>Ein SerializableData object, sollte der Nutzer im FileDialog abbrechen, dann null</returns>
        public static SerializableDataObject? ImportXMLData()
        {
            try
            {
                // Öffne einen FileDialog und frage den User nach einer XML-Datei zum Laden
                string pathToLoadFile;
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "XML Datei|*.xml";
                if (fileDialog.ShowDialog() == true)
                {
                    pathToLoadFile = fileDialog.FileName;
                }
                else
                {
                    // Wenn der file dialog abgebrochen wurde, dann null
                    return null;
                }
                // Deserialisiere Objekt
                XmlSerializer xmlDeserializer = new XmlSerializer(typeof(SerializableDataObject));
                TextReader textReader = new StreamReader(pathToLoadFile);
                return (SerializableDataObject)xmlDeserializer.Deserialize(textReader);
            } catch(Exception exception)
            {
                Debug.WriteLine(exception.StackTrace);
                ((MetroWindow)App.Current.MainWindow).ShowMessageAsync("Fehler", "Ein Fehler ist aufgetreten: " + exception.Message, MessageDialogStyle.Affirmative);
                return null;
            }
        }                     
    }
}
