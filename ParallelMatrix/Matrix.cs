using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Console;

// Изменить свойства на readonly \/
// try... catch return null \/
// границы индексатора
// отрицательные размеры матрицы или квадратной матрицы


namespace ParallelMatrix
{
    internal class Matrix
    {
        public virtual int? Row => Elems?.GetLength(1); 
        public virtual int? Col => Elems?.GetLength(0);
        public virtual double[,]? Elems { get; set; }


        public Matrix(double?[,] mat)
        {
            try
            {
                Elems = new double[mat.GetLength(1), mat.GetLength(0)];
                for (int i = 0; i < mat.GetLength(1); i++)
                {
                    for (int j = 0; j < mat.GetLength(0); j++)
                    {
                        Elems[i, j] = mat[i, j];
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
        }

        public Matrix(int row, int col)
        {
            Elems = new double[row, col];
        }
        // Конструктор копирования
        public Matrix(Matrix other)
        {
            try
            {
                this.Elems = other.Elems;
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
                Elems = new double[0, 0];
            }
        }
        // Индексатор
        public double this[int i, int j]
        {
            get
            {
                if (i <= Elems.GetLength(1) && j <= Elems.GetLength(0) )//&& Elems != null)
                {
                    return Elems[i, j];
                }
                return 0;
            }
            set
            {
                Elems[i, j] = value;
            }
        }

        // Оператор сложения двух матриц
        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if ((m1.Row == m2.Row) && (m1.Col == m2.Col))
            {
                Matrix m3 = new Matrix(m1.Row, m1.Col);
                for (int i = 0; i < m3.Row; i++)
                {
                    for (int j = 0; j < m3.Col; j++)
                    {
                        m3[i, j] = m1[i, j] + m2[i, j];
                    }
                }
                return m3;
            }
            else
            {
                return new Matrix(0, 0);
            }
        }
        // Оператор вычитания двух матриц
        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            if ((m1.Row == m2.Row) && (m1.Col == m2.Col))
            {
                Matrix m3 = new Matrix(m1.Row, m1.Col);
                for (int i = 0; i < m3.Row; i++)
                {
                    for (int j = 0; j < m3.Col; j++)
                    {
                        m3[i, j] = m1[i, j] - m2[i, j];
                    }
                }
                return m3;
            }
            else
            {
                return new Matrix(0, 0);
            }
        }
        // Оператор умножения матрицы на число

        public static Matrix operator *(Matrix m1, double n)
        {
            Matrix m3 = new Matrix(m1.Row, m1.Col);
            for (int i = 0; i < m3.Row; i++)
            {
                for (int j = 0; j < m3.Col; j++)
                {
                    m3[i, j] = m1[i, j] * n;
                }
            }
            return m3;
        }
        // Оператор умножения матриц

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.Col == m2.Row)
            {
                Matrix m3 = new Matrix(m1.Row, m2.Col);
                for (int i = 0; i < m3.Row; i++)
                {
                    for (int j = 0; j < m3.Col; j++)
                    {
                        m3[i, j] = FindElemOfMatrix(m1, m2, m1.Col, i, j);
                    }
                }
                return m3;
            }
            else
            {
                return new Matrix(0, 0);
            }
        }
        protected static double FindElemOfMatrix(Matrix m1, Matrix m2, int r, int i, int j)
        {
            double sum = 0;
            int w = 0;
            while (w != r)
            {
                sum += m1[i, w] * m2[w, j];
                w++;
            }
            return sum;
        }
        // Функция транспонирования матрицы
        public virtual Matrix Transp()
        {
            Matrix m3 = new Matrix(this.Col, this.Row);
            for (int i = 0; i < m3.Row; i++)
            {
                for (int j = 0; j < m3.Col; j++)
                {
                    m3[i, j] = this[j, i];
                }
            }
            return m3;
        }

        // Перевод матицы в строку
        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < this.Row; i++)
            {
                for (int j = 0; j < this.Col; j++)
                {
                    s += $"{this[i, j]} \t";
                }
                s += "\n";
            }
            return s;
        }

        public override int GetHashCode()
        {
            double[,] a = this.Elems;
            return HashCode.Combine(a);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Matrix)
            {
                foreach (double d1 in this.Elems)
                {
                    foreach (double d2 in ((Matrix)obj).Elems)
                    {
                        if (d1 != d2)
                        {
                            return false;
                        }
                        return true;
                    }

                }
            }
            return false;
        }

        public static bool operator ==(Matrix mat1, Matrix mat2) => (mat1.Equals(mat2));
        public static bool operator !=(Matrix mat1, Matrix mat2) => !(mat1 == mat2);
    }
}
