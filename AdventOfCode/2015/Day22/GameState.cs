namespace AdventOfCode._2015.Day22
{
    public enum Spell
    {
        MagicMissile,
        Drain,
        Shield,
        Poision,
        Recharge
    }

    public class GameState
    {
        private int _bossHealth;
        private int _playerHealth;

        public int BossHealth
        {
            get => _bossHealth;
            set
            {
                _bossHealth = value;
                if (_bossHealth <= 0)
                    GameEnded = true;
            }
        }
        public int BossDamage { get; set; }
        public int PlayerHealth
        {
            get => _playerHealth;
            set
            {
                _playerHealth = value;
                if (PlayerHealth <= 0)
                    GameEnded = true;
            }
        }
        public int PlayerMana { get; set; }
        public int ShieldDuration { get; set; }
        public int PoisionDuration { get; set; }
        public int RechargeDuration { get; set; }
        public int TotalManaSpent { get; set; }
        public bool GameEnded { get; set; }

        public bool PlayerWon => PlayerHealth > 0 && BossHealth <= 0;
        public bool OutOfMana => PlayerMana < 53 && RechargeDuration == 0;
        public int NextTurnMana => PlayerMana + (RechargeDuration > 0 ? 101 : 0);

        public GameState(int bossHp, int bossDmg, int playerHealth, int playerMana)
        {
            BossHealth = bossHp;
            BossDamage = bossDmg;
            PlayerHealth = playerHealth;
            PlayerMana = playerMana;
        }

        public GameState(GameState other)
        {
            BossHealth = other.BossHealth;
            BossDamage = other.BossDamage;
            PlayerHealth = other.PlayerHealth;
            PlayerMana = other.PlayerMana;
            ShieldDuration = other.ShieldDuration;
            PoisionDuration = other.PoisionDuration;
            RechargeDuration = other.RechargeDuration;
            TotalManaSpent = other.TotalManaSpent;
            GameEnded = other.GameEnded;
        }

        public void CastSpell(Spell spell)
        {
            if (OutOfMana || GameEnded)
                return;

            switch (spell)
            {
                case Spell.MagicMissile:
                    PlayerMana -= 53;
                    TotalManaSpent += 53;
                    BossHealth -= 4;
                    break;

                case Spell.Drain:
                    PlayerMana -= 73;
                    TotalManaSpent += 73;
                    BossHealth -= 2;
                    PlayerHealth += 2;
                    break;

                case Spell.Shield:
                    if (ShieldDuration == 0)
                    {
                        PlayerMana -= 113;
                        TotalManaSpent += 113;
                        ShieldDuration = 6;
                    }
                    break;

                case Spell.Poision:
                    if (PoisionDuration == 0)
                    {
                        PlayerMana -= 173;
                        TotalManaSpent += 173;
                        PoisionDuration = 6;
                    }
                    break;

                case Spell.Recharge:
                    if (RechargeDuration == 0)
                    {
                        PlayerMana -= 229;
                        TotalManaSpent += 229;
                        RechargeDuration = 5;
                    }
                    break;
            }
        }

        public void TriggerCooldowns()
        {
            if (ShieldDuration > 0)
                ShieldDuration--;

            if (PoisionDuration > 0)
            {
                BossHealth -= 3;
                PoisionDuration--;
            }

            if (RechargeDuration > 0)
            {
                PlayerMana += 101;
                RechargeDuration--;
            }
        }

        public IEnumerable<Spell> AvailableSpells()
        {
            if (NextTurnMana >= 53)
                yield return Spell.MagicMissile;

            if (NextTurnMana >= 73)
                yield return Spell.Drain;

            if (ShieldDuration <= 1 && NextTurnMana >= 113)
                yield return Spell.Shield;

            if (PoisionDuration <= 1 && NextTurnMana >= 173)
                yield return Spell.Poision;

            if (RechargeDuration <= 1 && NextTurnMana >= 229)
                yield return Spell.Recharge;
        }

        public GameState CreateNextState(Spell spell, bool isHardMode = false)
        {
            var newState = new GameState(this);

            // player turn
            if (isHardMode)
            {
                newState.PlayerHealth--;
                if (newState.GameEnded)
                    return newState;
            }

            newState.TriggerCooldowns();
            if (newState.GameEnded)
                return newState;

            newState.CastSpell(spell);
            if (newState.GameEnded)
                return newState;

            // boss turn
            newState.TriggerCooldowns();
            if (newState.GameEnded)
                return newState;

            newState.PlayerHealth -= Math.Max(1, newState.BossDamage - (newState.ShieldDuration > 0 ? 7 : 0));
            return newState;
        }
    }
}
