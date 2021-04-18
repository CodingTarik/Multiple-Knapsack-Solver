using BWI_Hardwareverteilung.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BWI_Hardwareverteilung.Util.Data
{
    /// <summary>
    /// Eine simple Klasse bzw. Struktur zum Serialisieren der zu transportierenden Objekte und der Transporterdaten. (Export-Data)
    /// </summary>
    [Serializable]
    public struct SerializableDataObject
    {
        public Collection<TransportableObject> transportableObjects;
        public Collection<Transporter> transporter;
    }
}
