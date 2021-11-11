using UnityEngine;

public class Fruit : Entity<Fruit>
{
    [SerializeField] int _points = 100;

    public int Points => _points;
}