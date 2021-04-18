using BWI_Hardwareverteilung.Models;
using BWI_Hardwareverteilung.Util.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;

namespace BWI_Hardwareverteilung.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        // Privates Attribut für die Property TransporterList
        private ObservableCollection<Transporter> _transporterList = new ObservableCollection<Transporter>();

        /// <summary>
        /// Diese Liste beinhaltet alle Transporter. Die Liste ist mit der GUI verbunden. Das DatenGrid der GUI
        /// fügt hier also automatisch neue Transporter-Objekte hinzu, wenn der User dies in der GUI tut.
        /// </summary>
        public ObservableCollection<Transporter> TransporterList { get => _transporterList; set { _transporterList = value; NotifyPropertyChanged(); } }

        // Privates Attribut für die Property TransportableObjectsList
        private ObservableCollection<TransportableObject> _transportableObjectsList = new ObservableCollection<TransportableObject>();

        /// <summary>
        /// Diese Liste beinhaltet alle TransportObjekte die im Lager zur Verfügugn stehen. Die Liste ist mit der GUI verbunden. Das DatenGrid der GUI
        /// fügt hier also automatisch neue TransporteObjekte hinzu, wenn der User dies in der GUI tut.
        /// </summary>
        public ObservableCollection<TransportableObject> TransportableObjectsList { get => _transportableObjectsList; set { _transportableObjectsList = value; NotifyPropertyChanged(); } }

        /// <summary>
        /// Dieses Event muss durch das INotifyPropertyChanged Interface implementiert werden
        /// jedes mal, wenn sich eine Eigenschaft ändert, also die Transporterliste oder die Objektliste, muss
        /// dieses Event "gefeuert" werden, damit die GUI weiß, dass es Änderungen der Daten gab und diese sich so aktualisieren kann.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Kann ausgeführt werden, wenn sich eine Eigenschaft ändert und feuert dann das zugehörige PropertyChanged event
        /// ==> Wird zum aktualisieren der Daten in der GUI benötigt
        /// </summary>
        /// <param name="propertyName"></param>
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// Methode zum exportieren der Transporterliste und Objektliste
        /// </summary>
        public void ExportData()
        {
            Util.Data.SaveData.SaveInputData(TransportableObjectsList, TransporterList);
        }

        /// <summary>
        /// Methode zum importieren einer einer vorher exportierten Speicherdatei
        /// </summary>
        public void ImportData()
        {
            SerializableDataObject? deserializedObject = Util.Data.ImportData.ImportXMLData();
            // Wenn null, dann wurde im Speicher-Menü abgebrochen o. es gab einen Fehler beim Serialisieren
            // verändert wird also nichts
            if(deserializedObject == null)
            {
                return;
            }
            else
            {
                SerializableDataObject data = (SerializableDataObject)deserializedObject;
                TransportableObjectsList = new ObservableCollection<TransportableObject>(data.transportableObjects);
                TransporterList = new ObservableCollection<Transporter>(data.transporter);
            }
        }

        /// <summary>
        /// Gesamtscore für alle Trucks
        /// </summary>
        public int ResultScore { get { return TransporterList.Sum(transporter => transporter.ResultScore); }  set { } }
    }
}
