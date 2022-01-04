using System;

namespace physics
{
    // Hold a vector in two-dimensions.
    class Vector2D
    {
        // Store the x-coordinates of the vector.
        public double X;

        // Store the y-coordinates of the vector.
        public double Y;

        // Default constructor, create a zero vector.
        public Vector2D()
        {
            X = 0;
            Y = 0;
        }

        // Explicit constructor creates a vector with the given arguments.
        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        // Flip the components of this vector.
        public void Invert()
        {
            X = -X;
            Y = -Y;
        }

        // Calculate the magnitude of the vector.
        public double Magnitude()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        // Calculate the squared magnituded of this vector.
        // This is used for slower machines to not calculate sqrt.
        public double SquareMagnitude()
        {
            return X * X + Y * Y;
        }

        // Normalises vector.
        public void Normalise()
        {
            double length = Magnitude();
            // Avoids 0 division
            if (length > 0)
            {
                X *= 1 / length;
                Y *= 1 / length;
            }
        }

        // Adds a given value to each component of the vector.
        public void AddScalar(double scalar)
        {
            X += scalar;
            Y += scalar;
        }

        // Subtracts a given value from each component of the vector.
        public void SubtractScalar(double scalar)
        {
            AddScalar(-scalar);
        }

        // Multiplies the vector by a given scalar.
        public void MultiplyByScalar(double scalar)
        {
            X *= scalar;
            Y *= scalar;
        }

        // Divides the vector by a given scalar.
        public void DivideByScalar(double scalar)
        {
            MultiplyByScalar(1 / scalar);
        }

        // Adds a given vector to the vector.
        public void AddVector(Vector2D vec)
        {
            X += vec.X;
            Y += vec.Y;
        }

        // Subtract a given vector from the vector.
        public void SubtracctVector(Vector2D vec)
        {
            X -= vec.X;
            Y -= vec.Y;
        }

        // Calculates the component product of two vectors.
        // Return a vector.
        public void ComponentProduct(Vector2D vec)
        {
            X *= vec.X;
            Y *= vec.Y;
        }

        // Calculate the scalar product with a given vector.
        // Return a scalar.
        public double SclarProduct(Vector2D vec)
        {
            return (X * vec.X) + (Y * vec.Y);
        }

        // Return a vector perpendicular to this vector.
        public Vector2D PerpendicularAnticlockwise()
        {
            return new Vector2D(-Y, X);
        }

        // Return a vector perpendicular to this vector.
        public Vector2D PerpendicularClockwise()
        {
            return new Vector2D(Y, -X);
        }

        // Use instead of AddVector() to return the sum of two vectors
        public static Vector2D operator + (Vector2D vec1, Vector2D vec2)
        {
            return new Vector2D((vec1.X + vec2.X), (vec1.Y + vec2.Y));
        }

        // Use instead of MultiplyByScalar() to return the product of a vector and a scalar.
        public static Vector2D operator * (Vector2D vec, double scalar)
        {
            return new Vector2D((vec.X * scalar), (vec.Y * scalar));
        }


        // Use instead of SubtractVector().
        public static Vector2D operator - (Vector2D vec1, Vector2D vec2)
        {
            return new Vector2D((vec1.X - vec2.X), (vec1.Y - vec2.Y));
        }

        public static Vector2D operator / (Vector2D vec, double scalar)
        {
            return new Vector2D((vec.X * 1/scalar), (vec.Y * 1/scalar));
        }
    }
}
