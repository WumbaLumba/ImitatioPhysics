using System.Collections.Generic;
using System.Linq;

namespace ImitatioPhysics
{
    class ShapesToRender
    {
        //private List<Shape> _shapes = new List<Shape>();
        private Dictionary<string, Shape> _shapes = new Dictionary<string, Shape>();

        public ShapesToRender()
        {
            
        }

        public void AddShape(string label, Shape shape)
        {
            _shapes.Add(label, shape);
        }

        public Shape SelectShape(string label)
        {
            // Extract the key where the element value is equal to the label
            return _shapes.FirstOrDefault(x => x.Key == label).Value;
        }
    }
}
