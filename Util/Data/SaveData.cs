using BWI_Hardwareverteilung.Models;
using BWI_Hardwareverteilung.Util.Data;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using MahApps.Metro.Controls.Dialogs;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace BWI_Hardwareverteilung.Util.Data
{
    /// <summary>
    /// Diese Klasse übernimmt das abspeichern bzw. sichern der Daten (Speichern-Button in der GUI)
    /// </summary>
    class SaveData
    {      
        /// <summary>
        /// Diese Methode öffnet ein Speichern-Dialog und serialisiert die zu transportierende Objekte und die Transporterdaten als XML-Datei
        /// </summary>
        /// <param name="transportableObjects"></param>
        /// <param name="transporter"></param>
        public static void SaveInputData(Collection<TransportableObject> transportableObjects, Collection<Transporter> transporter)
        {
            try
            {
                // Öffne ein Speichern-Dialog, zum speichern der Datei
                string pathToSaveFile;
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "XML Datei|*.xml";
                if (saveFileDialog.ShowDialog() == true)
                {
                    pathToSaveFile = saveFileDialog.FileName;
                }
                else
                {
                    // Wenn im Speichern-Menü abgebrochen wurde, liefert ShowDialog() false zurück d.h. speichern wir unsere Datei nicht
                    return;
                }

                // Erstelle neues Serialisierungsobjekt
                SerializableDataObject dataObject = new SerializableDataObject() { transportableObjects = transportableObjects, transporter = transporter };

                // Serialisiere Objekt
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(SerializableDataObject));
                TextWriter fileTextWriter = new StreamWriter(pathToSaveFile);
                xmlSerializer.Serialize(fileTextWriter, dataObject);
                fileTextWriter.Close();
            } catch(Exception exception)
            {
                Debug.WriteLine(exception.StackTrace);
                ((MetroWindow)App.Current.MainWindow).ShowMessageAsync("Fehler", "Ein Fehler ist aufgetreten: " + exception.Message, MessageDialogStyle.Affirmative);
            }
        }
    }
}
