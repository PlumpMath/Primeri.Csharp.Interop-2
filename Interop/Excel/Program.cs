using System;

namespace Excel
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			DataStruct data = new DataStruct ();
			IOWrite write = new IOWrite (data);


			// Nabirane na danni v osvnovnata tablica
			data.addRow ( "Martin" , "Simeonov" , "33" );
			data.addRow ( "Georgi" , "Marinov" , "21" );


			//Proverka na tablicata
			data.printTable ();
			write.exportTable ();
			write.runFile ();
		}
	}
}
