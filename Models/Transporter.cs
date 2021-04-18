using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BWI_Hardwareverteilung.Models
{
    [Serializable]
    public class Transporter : INotifyPropertyChanged
    {
        /// <summary>
        /// Name für den Truck
        /// </summary>
        public string TruckName { get; set; }

        /// <summary>
        /// Die Maximale Kapazität des Transporters (Produkte & Gewicht des Fahrers) in KG
        /// </summary>
        public int MaxCapacity { get; set; }

        /// <summary>
        /// Das Gewicht des Fahrers in g
        /// </summary>
        public float WeightOfDriver { get; set; }

        /// <summary>
        /// Objekte in einem Truck
        /// </summary>
        [Browsable(false)]
        public List<TransportableObject> ObjectList { get; set; } = new List<TransportableObject>();

        /// <summary>
        /// Ergebnistext für die Ausgabe
        /// </summary>
        [Browsable(false)]
        public string ResultText
        {
            get
            {
                if (ObjectList != null)
                {
                    string output = "Maximale Kapazität: " + MaxCapacity + "KG\r\nGewicht des Fahrers: " + WeightOfDriver + "KG\r\n" + "Gewicht für Transportobjekte: " + (MaxCapacity - WeightOfDriver) + "KG"
                        + "\r\nKG\r\n---\r\nObjekte:";
                    foreach (TransportableObject obj in ObjectList.GroupBy(item => item.Name).Select(item => item.First()).ToList())
                    {
                        output = output + "\r\n" + obj.Name + " Anzahl: " + ObjectList.Where(item => item.Name == obj.Name).Count();
                    }
                    output += "\r\n---\r\n" + "Score für Truck: " + ResultScore;
                    output += "\r\nVerbleibendes Gewicht für Elemente für Truck: " + (MaxCapacity * 1000 - WeightOfDriver * 1000 - ObjectList.Sum(item => item.Weight) + "g");
                    return output;
                }
                return "error";
            }
            set { }
        }

        /// <summary>
        /// Score für alle Items, die der Transporter lagert
        /// </summary>
        [Browsable(false)]
        public int ResultScore => ObjectList.Sum(item => item.Usefullness);

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
    }
}
