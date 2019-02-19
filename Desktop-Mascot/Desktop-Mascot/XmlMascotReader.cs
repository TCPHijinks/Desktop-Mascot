using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Desktop_Mascot
{
	class XmlMascotReader
	{
		public string MascotName { get; private set; }
        public float MascotDecelerationX { get; private set; }
        public float MascotDecelerationY { get; private set; }

        public int MascotGravity { get; private set; }		
		public int MascotMaxForceX { get; private set; }
		public int MascotMaxForceY { get; private set; }

		XmlDocument actionsDoc;


		/// <summary>
		/// Loads all mascot physics settings.
		/// </summary>
		/// <param name="mascotName"></param>
		public XmlMascotReader(string mascotName)
		{
			string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "Data\\conf\\actions.xml";

			// Open actions xml file.
			actionsDoc = new XmlDocument();
			actionsDoc.Load(xmlPath);
					
			XmlNode node = actionsDoc.DocumentElement.SelectSingleNode("//Mascot[@name='"+mascotName+"']//Physics");
			MascotGravity = Int32.Parse(node.Attributes["gravity"].Value);

			// Load mascot x-axis physics settings. 
			node = actionsDoc.DocumentElement.SelectSingleNode("//Mascot[@name='"+mascotName+"']//Physics/xAxis");			
			MascotDecelerationX = float.Parse(node.Attributes["deceleration"].Value);
			MascotMaxForceX = Int32.Parse(node.Attributes["maxForce"].Value);

			// Load mascot y-axis physics settings.
			node = actionsDoc.DocumentElement.SelectSingleNode("//Mascot[@name='"+mascotName+"']//Physics/yAxis");
			MascotDecelerationY = float.Parse(node.Attributes["deceleration"].Value);
			MascotMaxForceY = Int32.Parse(node.Attributes["maxForce"].Value);

			MascotName = mascotName;
		}

		/// <summary>
		/// Returns a wanted attribute from a given node address.
		/// </summary>
		/// <param name="nodePath"></param>
		/// <param name="wantedAttribute"></param>
		/// <returns></returns>
		public string GetSingleNode(string nodePath, string wantedAttribute)
		{
			XmlNode node = actionsDoc.DocumentElement.SelectSingleNode(nodePath);			
			return node.Attributes[wantedAttribute].Value;
		}
	}
}
