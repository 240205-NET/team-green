using System;
using System.Net.Http;
using System.Text.Json;

namespace Toasted.App
{
	public class Program
	{

		public static void Main(string[] args)
		{
			Toasted toasted = new Toasted();
			toasted.Run();
		}
	}

}

