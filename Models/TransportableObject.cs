using System;
using System.Collections.Generic;
using System.Text;

namespace BWI_Hardwareverteilung.Models
{
    /// <summary>
    /// Diese Klasse bildet die nötigsten Eigenschaften eines transportierbaren Objektes ab 
    /// </summary>
    /// <example>
    /// Ein Beispiel für ein solches Objekt ist ein Laptop oder ein Tablet
    /// </example>
    [Serializable]
    public class TransportableObject
    {
        /// <summary>
        /// Name des Objektes
        /// </summary>
        /// <example>
        /// zum Beispiel 'Tablet Büro groß'
        /// </example>
        public string Name { get; set; }

        /// <summary>
        /// Spiegelt die benötigte Anzahl des jeweiligen Objektes wieder
        /// </summary>
        public int RequiredNumber { get; set; }

        /// <summary>
        /// Das Gewicht eines Objektes mit Verpackung und Zubehör in Gramm
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Spiegelt die Nützlichkeit eines Elementes wieder, je höher dieser Wert ist, desto nützlicher ist ein Objekt dieser Art
        /// </summary>
        public int Usefullness { get; set; }
    }
}
