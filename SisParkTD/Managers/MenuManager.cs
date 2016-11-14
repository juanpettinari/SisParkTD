using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SisParkTD.Managers
{
    public class MenuManager
    {
        private List<MenuList> GetMenu()
        {

            //TODO create Menu model and add data to database
            //TODO crear el algoritmo que cree el arbol

            //traer todos los item de menu en la base de datos
            //filtrar por role
            //definir algoritmo para armar el arbol
            //crear la lista de menulist
            //recorrer la lista de items y llenar la lista de menulist con root.Id == item.FatherId(base de datos)

            var menuList = new List<MenuList>();
            // Create a tree structure
            MenuList root = new MenuList("root");
            root.Add(new MenuItem("Leaf A"));
            root.Add(new MenuItem("Leaf B"));
            
            MenuList comp = new MenuList("Composite X");
            comp.Add(new MenuItem("Leaf XA"));
            comp.Add(new MenuItem("Leaf XB"));

            root.Add(comp);
            root.Add(new MenuItem("Leaf C"));

            // Add and remove a leaf
            MenuItem leaf = new MenuItem("Leaf D");
            root.Add(leaf);
            root.Remove(leaf);

            //// Recursively display tree
            //root.Display(1);

            //// Wait for user
            //Console.ReadKey();

            return menuList;
        }

    }

    /// <summary>
    /// The 'Component' abstract class
    /// </summary>
    abstract class Component
    {
        protected string name;

        // Constructor
        public Component(string name)
        {
            this.name = name;
        }

        public abstract void Add(Component c);
        public abstract void Remove(Component c);
        public abstract void Display(int depth);
    }

    /// <summary>
    /// The 'Composite' class
    /// </summary>
    class MenuList : Component
    {
        private List<Component> _children = new List<Component>();
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Father { get; set; }

        // Constructor
        public MenuList(string name)
          : base(name)
        {
        }

        public override void Add(Component component)
        {
            _children.Add(component);
        }

        public override void Remove(Component component)
        {
            _children.Remove(component);
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + name);

            // Recursively display child nodes
            foreach (Component component in _children)
            {
                component.Display(depth + 2);
            }
        }
    }

    /// <summary>
    /// The 'Leaf' class
    /// </summary>
    class MenuItem : Component
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Father { get; set; }



        // Constructor
        public MenuItem(string name)
          : base(name)
        {
        }

        public override void Add(Component c)
        {
            Console.WriteLine("Cannot add to a leaf");
        }

        public override void Remove(Component c)
        {
            Console.WriteLine("Cannot remove from a leaf");
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + name);
        }
    }
}