using System;
using System.IO;

namespace LocalAlignment
{
  class Program
  {

    static void Main()
    {
      string seq1 = "ACACACTA";
      string seq2 = "AGCACACA";

      var result = LocalAligner.Align(seq1, seq2, "resultado.txt");

      Console.WriteLine("\nScore final: " + result.Score);
      foreach (var (alignedA, alignedB) in result.Alignments)
      {
        Console.WriteLine("Alineamiento 1: " + alignedA);
        Console.WriteLine("Alineamiento 2: " + alignedB);
      }
    }
  }
}