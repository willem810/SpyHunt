using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class HumanManager : MonoBehaviour
    {
        public int NrSpies = 1;

        public List<Human> Humans = new List<Human>();
        private void Awake()
        {
            getHumans();
            GiveRoles(NrSpies);
        }

        private void Start()
        {
            SetDisguised(false);
        }

        private void getHumans()
        {
            Humans.AddRange(FindObjectsOfType<Human>());
        }

        public void SetDisguised(bool disguise)
        {
            Humans.ForEach(h => h.SetDisguised(disguise));
        }

        public List<Human> GetHumans(Role role)
        {
            return Humans.FindAll(h => h.role == role);
        }

        public List<Human> GetHumans()
        {
            return Humans;
        }

        private void GiveRoles(int nrSpies)
        {
            System.Random r = new System.Random();
            var randomisedHumans = Humans.OrderBy(x => r.Next());

            List<Human> spies = randomisedHumans.Take(nrSpies).ToList();
            List<Human> innocent = randomisedHumans.Except(spies).ToList();

            spies.ForEach(h => h.SetRole(Role.Spy));
            innocent.ForEach(h => h.SetRole(Role.Innocent));

        }
    }
}
