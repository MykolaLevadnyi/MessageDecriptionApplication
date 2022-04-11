using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TelecomLab1
{
	class Program
	{
		static void Main(string[] args)
		{
			try {
				int wCodeIndex = 13;
				int hadamarMatrixSize = 16;

				//Hadamard matrix generation
				var hadamardMatrix = Hadamard.generateHadamardMatrix(hadamarMatrixSize);
				Console.WriteLine("Hadamard matrix:\n" +
					string.Join("\n", hadamardMatrix.Select(line =>
						string.Join(", ", line.Select(number => string.Format("{0,2}", number)))))
					+ "\n");

				List<int> WCode = hadamardMatrix[wCodeIndex];
				Console.WriteLine($"Warshall code: ({string.Join(", ", WCode)})\n");

				// Read encrypted sequence from file
				StreamReader objInput = new StreamReader("DataBinary.dat", System.Text.Encoding.Default);
				string contents = objInput.ReadToEnd().Trim();
				string[] split = Regex.Split(contents, "\\s+", RegexOptions.None);
				List<int> EncryptedSequence = split.Select(n => int.Parse(n)).ToList();

				if (EncryptedSequence.Count % WCode.Count != 0) {
					throw new Exception("Encrypted sequence have wrong size!");

				}

				Console.WriteLine($"Encrypted sequence: ({string.Join(", ", EncryptedSequence)})\n");

				// Split Encrypted sequence
				List<List<int>> EncryptedSequenceParts = EncryptedSequence.ChunkBy(WCode.Count).Select(p => p.ToList()).ToList();

				List<int> CC = new List<int>();
				// Count correlations
				for (int i = 0; i < EncryptedSequenceParts.Count; i++) {
					var encryptedSequencePart = EncryptedSequenceParts[i];
					var CC_i = encryptedSequencePart.Select((value, index) => value * WCode.ElementAt(index)).Sum() / hadamarMatrixSize;
					CC.Add(CC_i);

					Console.WriteLine($"CC{i}: {CC_i}");
				}
				Console.WriteLine();

				// Rebase to binary
				List<int> CCBinary = CC.Select(v => v == 1 ? 0 : 1).ToList();

				Console.WriteLine($"Decrypted binary sequence: ({string.Join(", ", CCBinary)})\n");
			} catch (Exception ex) {
				Console.WriteLine(ex.Message);
			}
		}
	}
}
