public class BombSpawner : Spawner<Bomb>
{
    public Bomb GetBomb()
    {
        return Spawn();
    }

    public void ReturnBomb(Bomb bomb)
    {
        Despawn(bomb);
    }
}