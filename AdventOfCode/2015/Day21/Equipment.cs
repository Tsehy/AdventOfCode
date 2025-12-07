namespace AdventOfCode._2015.Day21
{
    public class Equipment(int price, int damage, int armor)
    {
        public int Price { get { return price; } }
        public int Damage { get { return damage; } }
        public int Armor { get { return armor; } }
    }

    public class EquipmentSetup
    {
        public Equipment? Weapon { get; set; }
        public Equipment? Armor { get; set; }
        public Equipment? LeftRing { get; set; }
        public Equipment? RightRing { get; set; }

        public int TotalDamage => (Weapon?.Damage ?? 0) + (Armor?.Damage ?? 0) + (LeftRing?.Damage ?? 0) + (RightRing?.Damage ?? 0);
        public int TotalArmor => (Weapon?.Armor ?? 0) + (Armor?.Armor ?? 0) + (LeftRing?.Armor ?? 0) + (RightRing?.Armor ?? 0);
        public int TotalPrice => (Weapon?.Price ?? 0) + (Armor?.Price ?? 0) + (LeftRing?.Price ?? 0) + (RightRing?.Price ?? 0);

        public int TurnsToKill(int health, int armor)
        {
            int realDamage = Math.Max(1, TotalDamage - armor);
            return (int)Math.Ceiling((double)health / realDamage);
        }

        public int TurnsToDie(int damage)
        {
            int realDamage = Math.Max(1, damage - TotalArmor);
            return (int)Math.Ceiling(100.0 / realDamage);
        }

        public bool CanWinFight(int health, int damage, int armor)
        {
            return TurnsToKill(health, armor) <= TurnsToDie(damage);
        }
    }
}
