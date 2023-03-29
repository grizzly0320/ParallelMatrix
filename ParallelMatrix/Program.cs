using ParallelMatrix;
using System.Diagnostics;
using static System.Console;

Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();



Matrix mat1 = new Matrix(3, 4);
WriteLine(mat1.ToString());
//Matrix mat2 = new Matrix(1000, 1000);
//Matrix mat3 = mat1 + mat2;
//Matrix mat1 = new Matrix(1000);
//Matrix mat2 = new Matrix(1000);
//Matrix mat3 = mat1 * mat2;



stopwatch.Stop();
WriteLine(stopwatch.ElapsedMilliseconds);