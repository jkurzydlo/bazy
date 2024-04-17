using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazy1.Utils {
	public class PESELValidator {
		public PESELValidator() {
		}

		public static bool isValid(string PESEL) {
			if(PESEL.Length != 11) return false;
			//wagi zgodnie z https://www.gov.pl/web/gov/czym-jest-numer-pesel
			int[] wagi = [1, 3, 7, 9, 1, 3, 7, 9, 1, 3];
			int sum = 0;
			for(int i = 0; i < 10; i++)	
			{
				sum += wagi[i]* int.Parse(PESEL[i].ToString()); //mnożymy każdy ze znaków dla 10 pierwszych cyfr przez wagę i sumujemy wszystko
			}
			var reszta = 10 - sum %10 ; //obliczamy sumę kontrolną i porównujemy ją z ostatnią cyfrą
			var sumaKontrolna = (reszta == 10) ? 0 : reszta; //sprawdzamy czy taka sama suma kontrolna jest w ciągu
			if (PESEL[10] == sumaKontrolna) return true;
			else return false;
		}
	}
}
