//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ParallelMatrix
//{
//    internal class SquareMatrix : Matrix
//    {


//        public SquareMatrix(double[,] a) : base(a)
//        {
//            try
//            {
//                IsMatrixSquare(this);
//            }
//            catch (NotSquareMatrixException ex)
//            {
//                Console.WriteLine(ex.Message);
//                this.Elems = new double[0, 0];
//            }
//        }
//        public SquareMatrix(int row) : base(row, row)
//        {
//        }

//        public SquareMatrix(Matrix other) : base(other)
//        {
//            try
//            {
//                this.Elems = other.Elems;
//                IsMatrixSquare(this);
//            }
//            catch (NotSquareMatrixException ex)
//            {
//                Console.WriteLine(ex.Message);
//                this.Elems = new double[0, 0];
//            }
//        }
//        // Определитель
//        public double FindDet()
//        {

//            if (this.Row == 1)
//            {
//                return this[0, 0];
//            }
//            else if (this.Row == 2)
//            {
//                return this[0, 0] * this[1, 1] - this[1, 0] * this[0, 1];
//            }
//            else
//            {
//                double n = 0;
//                for (int j = 0; j < this.Row; j++)
//                {
//                    n += Math.Pow(-1, 1 + j) * this[1, j] * FindMinor(this, 1, j);
//                }
//                return n;
//            }
//        }
//        protected double FindMinor(SquareMatrix mat, int n, int m)
//        {
//            SquareMatrix mat_minor = new SquareMatrix(mat.Row - 1);
//            double[,] mas = new double[mat.Row, mat.Col];
//            for (int i = 0; i < mat.Row; i++)
//            {
//                for (int j = 0; j < mat.Col; j++)
//                {
//                    mas[i, j] = mat[i, j];
//                }
//            }
//            DelCol(DelRow(mas, n), m);
//            for (int i = 0; i < mat_minor.Row; i++)
//            {
//                for (int j = 0; j < mat_minor.Col; j++)
//                {
//                    mat_minor[i, j] = DelCol(DelRow(mas, n), m)[i, j];
//                }
//            }
//            return mat_minor.FindDet();

//        }
//        static double[,] DelRow(double[,] array, int row)
//        {
//            double[,] result = new double[array.GetLength(0) - 1, array.GetLength(1)];
//            int x = 0;
//            for (int i = 0; i < array.GetLength(0); i++)
//            {
//                if (i == row)
//                {
//                    row = -1;
//                    continue;
//                }

//                for (int j = 0; j < array.GetLength(1); j++)
//                {
//                    result[x, j] = array[i, j];
//                }
//                x++;
//            }
//            return result;
//        }
//        static double[,] DelCol(double[,] array, int col)
//        {
//            double[,] result = new double[array.GetLength(0), array.GetLength(1) - 1];
//            for (int i = 0; i < array.GetLength(0); i++)
//            {
//                int x = 0;
//                for (int j = 0; j < array.GetLength(1); j++)
//                {
//                    if (j == col) continue;
//                    result[i, x] = array[i, j];
//                    x++;
//                }
//            }
//            return result;
//        }

//        // Матрица алгебраических дополнений
//        private SquareMatrix FindAlgAddMatrix()
//        {

//            SquareMatrix mat_c = new SquareMatrix(this.Row);
//            for (int i = 0; i < Row; i++)
//            {
//                for (int j = 0; j < Col; j++)
//                {
//                    mat_c[i, j] = Math.Pow(-1, (i + j + 2)) * FindMinor(this, i, j);
//                }
//            }
//            return mat_c;
//        }

//        // Обратная матрица
//        public virtual SquareMatrix FindInverseMatrix()
//        {
//            IsMatrixSquare(this);
//            if (this.FindDet() != 0)
//                return (this.FindAlgAddMatrix().Transp() * (1 / this.FindDet()));
//            else
//            {
//                SquareMatrix null_mat = new SquareMatrix(1);
//                return null_mat;
//            }
//        }
//        // Функция транспонирования матрицы
//        public override SquareMatrix Transp()
//        {
//            SquareMatrix m3 = new SquareMatrix(this.Row);
//            for (int i = 0; i < m3.Row; i++)
//            {
//                for (int j = 0; j < m3.Col; j++)
//                {
//                    m3[i, j] = this[j, i];
//                }
//            }
//            return m3;
//        }
//        // Оператор сложения двух матриц
//        public static SquareMatrix operator +(SquareMatrix m1, SquareMatrix m2) => new SquareMatrix(m1 + m2);
//        // Оператор вычитания двух матриц
//        public static SquareMatrix operator -(SquareMatrix m1, SquareMatrix m2) => new SquareMatrix(m1 - m2);
//        // Оператор умножения матрицы на число
//        public static SquareMatrix operator *(SquareMatrix m1, double n) => new SquareMatrix(m1 * n);
//        // Оператор умножения матриц
//        public static SquareMatrix operator *(SquareMatrix m1, SquareMatrix m2) => new SquareMatrix(m1 * m2);
//        public static void IsMatrixSquare(SquareMatrix sqmat)
//        {
//            if (sqmat.Elems.GetLength(1) != sqmat.Elems.GetLength(0)) throw new NotSquareMatrixException();
//        }
//        public override bool Equals(object? obj)
//        {
//            if (obj is SquareMatrix)
//            {
//                foreach (double d1 in this.Elems)
//                {
//                    foreach (double d2 in ((SquareMatrix)obj).Elems)
//                    {
//                        if (d1 != d2)
//                        {
//                            return false;
//                        }
//                        return true;
//                    }

//                }
//            }
//            return false;
//        }

//        public override int GetHashCode()
//        {
//            return base.GetHashCode();
//        }
//    }
//    class NotSquareMatrixException : Exception
//    {
//        public NotSquareMatrixException() : base("Not Square Matrix")
//        {
//        }
//    }
//}
