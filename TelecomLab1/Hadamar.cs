using System;
using System.Collections.Generic;

namespace TelecomLab1
{
	public static class Hadamard
	{
		public static List<List<int>> generateHadamardMatrix(int size)
		{
			// Сheck if size is power of 2
			if(!IsPowerOfTwo(size)) {
				throw new Exception("Wrong size for Hadamard Matrix!");
			}

			// Matrix initialization
			List<List<int>> hadamarMatrix = new List<List<int>>();
			for (int i = 0; i < size; i++) {
				hadamarMatrix.Add(new List<int>());
				for (int j = 0; j < size; j++) {
					hadamarMatrix[i].Add(0);
				}
			}

			hadamarMatrix[0][0] = 1;
			for (int i = 1; i < size; i += i) {
				for (int j = 0; j < i; j++) {
					for (int k = 0; k < i; k++) {
						hadamarMatrix[j + i][k] = hadamarMatrix[j][k];
						hadamarMatrix[j][k + i] = hadamarMatrix[j][k];
						hadamarMatrix[j + i][k + i] = -hadamarMatrix[j][k];
					}
				}
			}	
			return hadamarMatrix;
		}

		private static bool IsPowerOfTwo(int number)
		{
			for (int x = 1; x <= number; x *= 2) {
				if (x == number) return true;
			}
			return false;
		}
	}
}
