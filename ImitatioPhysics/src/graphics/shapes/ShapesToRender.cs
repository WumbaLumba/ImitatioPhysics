using System;
using System.Collections.Generic;
using System.Linq;

namespace ImitatioPhysics
{
    class ShapesToRender
    {
        // Store all labelled shapes in a dictionary.
        private Dictionary<string, Shape> _shapes = new Dictionary<string, Shape>();
        private int _count = 0;

        public ShapesToRender()
        {
            
        }

        public void AddShape(string label, Shape shape)
        {
            if (label == "")
            {
                _count++;
                label = $"shape_{_count}";
            }

            try
            {
                _shapes.Add(label, shape);
            }
            catch (ArgumentException)
            {
                Console.WriteLine($"Shape {label} already exists.");
            }
        }

        public Shape SelectShape(string label)
        {
            // Extract the key where the element value is equal to the label.
            return _shapes.FirstOrDefault(x => x.Key == label).Value;
        }


    }
}