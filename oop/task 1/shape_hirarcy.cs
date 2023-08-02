using System;

class Shape
{
    protected string name;

    public Shape(string _name)
    {
        name = _name;
    }

    public virtual double CalculateArea()
    {
        return 0.0;
    }

    public string GetName()
    {
        return name;
    }
}

class Circle : Shape
{
    public double radius;

    public Circle(double _radius) : base("Circle")
    {
        radius = _radius;
    }

    public override double CalculateArea()
    {
        double area = Math.PI * radius * radius;
        return area;
    }
}
class Rectangle : Shape
{
    public double width;
    public double Height;

    public Rectangle(double _width ,double _height) : base("Rectangle")
    {
        width = _width;
        Height =_height;
    }

    public override double CalculateArea()
    {
        double area =  width * Height;
        return area;
    }
}


class Triangle : Shape
{
    public double Base;
    public double Height;

    public Triangle(double _base, double _height) : base("Triangle")
    {
        Base = _base;
        Height = _height;
    }

    public override double CalculateArea()
    {
        double area = (Base * Height)/2;
        return area;
    }
}




class Program
{
    static void PrintShapeArea(Shape shape)
    {
        double area = shape.CalculateArea();
        string name = shape.GetName();
        Console.WriteLine($"{name}'s area: {area}");
    }
    static void Main()
    {
        
        Shape circle = new Circle(5);

        double area = circle.CalculateArea();

        Shape Rectangle = new Rectangle(2,3);
        double areaOfRectangle =Rectangle.CalculateArea();

        Shape Triangle = new Triangle(2, 3);
        double areaOfTriangle = Triangle.CalculateArea();

        PrintShapeArea(Triangle);
        PrintShapeArea(Rectangle);
        PrintShapeArea(circle);

        ;
    }
}
