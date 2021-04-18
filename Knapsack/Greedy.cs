using BWI_Hardwareverteilung.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BWI_Hardwareverteilung.Knapsack
{
    /// <summary>
    /// Löst das Knapsackproblem Greedy
    /// </summary>
    internal class Greedy
    {
        private readonly List<TransportableObject> objects = new List<TransportableObject>();
        private readonly Transporter[] transporter;

        public Greedy(TransportableObject[] objectList, Transporter[] transporterList)
        {
            // Zuweisung der Transporter für die spätere Auswertung
            // Die Transporter werden von kleiner nach großer Kapazität sortiert, da große Transporter leichter zu befüllen sind
            transporter = transporterList.OrderBy(n => (n.MaxCapacity - n.WeightOfDriver)).ToArray();

            // Zuweisen der Profite und der Gewichte der einzelnen Objekte
            foreach (TransportableObject transportObject in objectList)
            {
                for (int i = 0; i < transportObject.RequiredNumber; i++)
                {
                    objects.Add(transportObject);
                }
            }

            // Sortiere nach dem Quotienten Profit / Gewicht   (Profit pro Gramm)
            // Je höher dieser Wert ist, desto besser
            objects = objects.OrderByDescending(n => n.Usefullness / n.Weight).ToList();

            // Lösen
            SolveGreedy();
        }

        private void SolveGreedy()
        {
            List<List<TransportableObject>> partition = new List<List<TransportableObject>>();

            // Gehe jeden Transporter tr durch
            for (int i = 0; i < transporter.Length; i++)
            {
                // Der aktuelle Transporter der befüllt wird
                Transporter tr = transporter[i];
                // Ausrechnung der maximalen Objektkapazität des Transporters aus MaxCapacity - WeightOfDriver * 1000 [Umrechnung KG in Gramm]
                double maxItemWeight = (tr.MaxCapacity - tr.WeightOfDriver) * 1000;
                // Derzeitige Objektladung des Transporters
                int curentItemWeight = 0;
                // Erstellen einer Liste um hinzugefügte Objekte für einen Transporter zu merken
                partition.Add(new List<TransportableObject>());

                // Gehe jedes verbleibende Element in der Objekt-Liste durch
                for (int x = 0; x < objects.Count;)
                {
                    TransportableObject obj = objects[x];
                    // Wenn noch genug 'Gewicht' frei für Objekt, dann füge es zur Liste hinzu
                    if (maxItemWeight - curentItemWeight - obj.Weight >= 0)
                    {
                        // Objekt zur Liste hinzufügen
                        partition[i].Add(obj);
                        // Objektgewicht um Objekt erhöhen
                        curentItemWeight += obj.Weight;
                        // Das Objekt steht nun für andere Transporter nicht mehr zur Verfügung, deshalb muss es rausgelöscht werden
                        objects.Remove(obj);
                        // Hier braucht man nun kein x++, da das neue Element nun an der aktuellen Position x ist
                    }
                    else
                    {
                        // Gehe zum nächsten Element, wenn kein neues hinzugefügt wurde
                        x++;
                    }
                }
            }

            // Optimieren der Greedy-Lösung
            for (int i = 0; i < transporter.Length; i++)
            {
                // Gehe alle nachfolgenden Transporter durch
                for (int x = i + 1; x < transporter.Length; x++)
                {
                    // Gibt es Objekte, in Transporter i, dass in Transporter x reinpasst?
                    TransportableObject obj;
                    while ((obj = partition[i].Where(item => item.Weight <= (transporter[x].MaxCapacity * 1000 - transporter[x].WeightOfDriver * 1000) - partition[x].Sum(itemAlreadyIn => itemAlreadyIn.Weight)).FirstOrDefault()) != null)
                    {
                        // Dann verschiebe diese Objekte in diesen Rucksack / Transporter
                        partition[i].Remove(obj);
                        partition[x].Add(obj);
                    }
                }
                // Passt in Transporter i nun doch noch ein oder mehere weitere Itemes / Objekte rein?
                TransportableObject newObj;
                while ((newObj = objects.Where(item => (transporter[i].MaxCapacity * 1000 - transporter[i].WeightOfDriver * 1000) - item.Weight - partition[i].Sum(itemAlreadyIn => itemAlreadyIn.Weight) >= 0).FirstOrDefault()) != null)
                {
                    // Dann füge diese dem Transporter hinzu
                    partition[i].Add(newObj);
                    // Natürlich muss dieses Objekt nun auch aus der Objektliste entfernt werden
                    objects.Remove(newObj);
                }
            }
            int overallUsefullness = 0;

            // Jetzt nur noch den Transporter die verschiedenen Listen zuweisen
            for (int i = 0; i < transporter.Length; i++)
            {
                int truckUsefullness = 0;
                Debug.WriteLine("Truck " + i + " :");
                transporter[i].ObjectList = partition[i];
                foreach (TransportableObject obj in partition[i].GroupBy(item => item.Name).Select(i => i.First()).ToList())
                {
                    Debug.WriteLine("Item: " + obj.Name + " count " + partition[i].Where(item => item.Name == obj.Name).Count() + " with weight " + obj.Weight + " and usefullness " + obj.Usefullness + " per item");
                    truckUsefullness += partition[i].Where(item => item.Name == obj.Name).Sum(item => item.Usefullness);
                }
                double weightLeftForTruck = transporter[i].MaxCapacity * 1000 - transporter[i].WeightOfDriver * 1000 - partition[i].Sum(item => item.Weight);
                Debug.WriteLine("========> truck usefullness: " + truckUsefullness + " ====> weight leftover (gramm): " + weightLeftForTruck);
                overallUsefullness += truckUsefullness;
            }
            Debug.WriteLine("Overall usefullness of transport: " + overallUsefullness);
        }
    }

}
