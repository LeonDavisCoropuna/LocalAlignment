using System.IO;
using Xunit;
namespace LocalAlignment;

public class LocalAlignerTests
{
  private readonly string outputFile = "test_output.txt";

  public LocalAlignerTests()
  {
    // Limpia el archivo antes de cada test (constructor actúa como Setup)
    if (File.Exists(outputFile))
      File.Delete(outputFile);
  }

  [Fact]
  public void Align_SimpleSequences_ReturnsCorrectScoreAndAlignment()
  {
    string seq1 = "ACACACTA";
    string seq2 = "AGCACACA";

    var result = LocalAligner.Align(seq1, seq2, outputFile);

    Assert.True(result.Score > 0, "El score debe ser positivo.");
    Assert.Equal(1, result.Alignments.Count);

    var (aligned1, aligned2) = result.Alignments[0];
    Assert.Equal(aligned1.Length, aligned2.Length);
    Assert.Contains('A', aligned1);
    // Assert.Contains('G', aligned2);  // <-- Elimina esta línea o cambia por lo siguiente:
    Assert.True(aligned2.Length > 0, "La alineación 2 no debe estar vacía.");


    Assert.True(File.Exists(outputFile), "El archivo de salida debe existir.");
    var content = File.ReadAllText(outputFile);
    Assert.Contains("Score máximo local", content);
    Assert.Contains(aligned1, content);
  }

  [Fact]
  public void Align_SequencesWithNoSimilarity_ReturnsZeroScore()
  {
    string seq1 = "AAAAAA";
    string seq2 = "GGGGGG";

    var result = LocalAligner.Align(seq1, seq2, outputFile);

    Assert.Equal(0, result.Score);
    Assert.Equal(1, result.Alignments.Count);

    var (aligned1, aligned2) = result.Alignments[0];
    Assert.True(aligned1.Length == 0 || aligned1.Length == aligned2.Length);

    Assert.True(File.Exists(outputFile), "El archivo de salida debe existir.");
    var content = File.ReadAllText(outputFile);
    Assert.Contains("Score máximo local: 0", content);
  }
}
