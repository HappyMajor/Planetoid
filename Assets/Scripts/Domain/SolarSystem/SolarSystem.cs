using System.Collections.Generic;

public class SolarSystem : Entity
{
    private List<Planet> planets;
    private List<Sun> suns;
    public List<Sun> Suns { get => suns; set => suns = value; }
    public List<Planet> Planets { get => planets; set => planets = value; }


}