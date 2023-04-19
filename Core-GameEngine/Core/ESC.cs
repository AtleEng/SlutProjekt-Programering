using System.Numerics;
using Raylib_cs;
using Core;
using Entities;
using Scripts;

namespace Core
{
    //Detta är den abstrakta classen Component - Components ger Entitys alla deras functioner som spelet kräver tex Sprites eller Colliders
    public abstract class Component
    {
        public Entity? entity; //Detta är den Entity som äger component:en, den sätts atomatiskt när man lägger till componenten
        public virtual void Start() { } //Start spelas (run) när objektet skapas och innan Update
        public virtual void OnDestroy() { } //Spelas innan objektet tas bort

        public virtual void Update() { } //Update spelas varje frame och är till logic
        public virtual void EditMode() { } //Som Update men för grafik

        public virtual void OnTrigger(Entity other) { } //Spelas när denna entity kolliderar (Mer som överlappa) med ett annat
    }
    //Detta är den abstrakta classen Entity - Entitys har componenter som ger dem functioner
    public abstract class Entity
    {
        public string name = "Entity"; //Entitys namn, används mest för debugging
        public Vector2 position = Vector2.Zero; //Positionen för Entity
        public List<Component> components = new(); //Listan med alla componenter till entity

        public virtual void Build() { }
        public void Start() //Start spelas (run) när objektet skapas och innan Update
        {
            foreach (Component component in components) //lopar igenom alla componenter
            {
                component.entity = this; //Sätter componentens entity till denna entity
                component.Start(); //Spela upp Start för varje component
            }
        }
        public void OnDestroy() //Spelas innan objektet tas bort
        {
            foreach (Component component in components) //lopar igenom alla componenter
            {
                component.OnDestroy();
            }
        }
        public void Update() //Update spelas varje frame och är till logic
        {
            foreach (Component component in components) //lopar igenom alla componenter
            {
                component.Update();
            }
        }
        public void EditMode() //Som Update men för grafik
        {
            foreach (Component component in components) //lopar igenom alla componenter
            {
                component.EditMode();
            }
        }
        public virtual void OnTrigger(Entity other) //Spelas när denna entity kolliderar (Mer som överlappa) med ett annat
        {
            foreach (Component component in components) //lopar igenom alla componenter
            {
                component.OnTrigger(other);
            }
        }
        //Components användning
        //-----------------------------------------------------
        //Denna method letar upp en component av samma typ tex Sprite och returnerar den, om den inte hittar någon returnerar den null
        public Type? GetComponent<Type>() where Type : Component //Type måste även härstamma från component
        {
            foreach (Component component in components) //lopar igenom alla componenter
            {
                if (component is Type typeComponent) //kollar om componenten är den typ man letar efter
                {
                    return typeComponent; //returnerar den component man letar efter
                }
            }
            return null; //Här måste man vara beredd att om det inte finns den componenten man letar efter kommer man få en error, detta kan man fixa med en null check tex if(component != null){logic}
        }
        //Denna method letar upp om entity har component. Om den har det --> true annars false
        public bool HasComponent<Type>() where Type : Component //Type måste även härstamma från component
        {
            foreach (Component component in components) //lopar igenom alla componenter
            {
                if (component is Type typeComponent) //kollar om componenten är den typ man letar efter
                {
                    return true; //returnerar true om entity har den component man letar efter
                }
            }
            return false; //annars retunerar false
        }
        //-----------------------------------------------------
    }
    //Detta är classen Manager - Den håller koll på alla entities. Den skapar och tar bort entities också.
    public static class Manager
    {
        public static List<Entity> entitiesInScene = new(); //Alla som används
        public static List<Collider> collidersInScene = new(); //Alla colliders som används
        public static List<Sprite> spritesInScene = new(); //Alla spritesInScene

        private static List<Entity> entitiesToAdd = new(); //Alla nya enties som skas läggas till
        private static List<Entity> entitiesToRemove = new(); //Alla enties som skas bort

        public static void Update()
        {
            //Ta bort och lägg till entities (Detta är för att unvika att förändra listan entitiesInScene när den lopas igenom vilket kan resultera i en error)
            //---------------------------------------------------------
            UpdateEntitiesList();
            //---------------------------------------------------------

            //Updatera entities
            //---------------------------------------------------------
            foreach (Entity entity in entitiesInScene) //lopar igenom alla entities
            {
                entity.Update(); //uppdatera alla
            }
            //---------------------------------------------------------
        }
        public static void Render()
        {
            foreach (Sprite sprite in spritesInScene) //lopar igenom alla sprites
            {
                if (sprite.isSpriteInView)
                {
                    sprite.Render(); //render alla
                }
            }
            if (GameWindow.isEditMode) //ritar ut några debug grejer som fps och antal entities
            {
                foreach (Entity entity in entitiesInScene) //lopar igenom alla entities
                {
                    entity.EditMode(); //uppdatera alla
                }
            }
        }
        public static void OnStart()
        {
            Spawn(new GameManager(), Vector2.Zero);
        }
        public static void Spawn(Entity entity, Vector2 spawnPos) //detta skapar en ny entity
        {
            entity.position = spawnPos; //sätter positionen till spawnPos
            entity.Build(); //Build är det process där entity "bygger sig själv" genom att lägga till och ändra components
            entity.Start(); //Aktivera start för den nya entity
            entitiesToAdd.Add(entity);
        }
        public static void Destroy(Entity entity) //ta bort entity
        {
            entitiesToRemove.Add(entity);
        }
        private static void UpdateEntitiesList()
        {
            foreach (Entity entity in entitiesToRemove) //lopar igenom alla entities i listan
            {
                try //Försöker ta bort entity, om det inte går ignorera och skriv ut i consolen
                {
                    entity.OnDestroy(); //Aktivera OnDestroy för det entity som ska tas bort
                    entitiesInScene.Remove(entity);
                    System.Console.WriteLine(entity.name + " has been removed");
                }
                catch
                {
                    System.Console.WriteLine(entity.name + " do not exist");
                }

            }
            entitiesToRemove.Clear(); //rensar listan 

            foreach (Entity entity in entitiesToAdd) //lopar igenom alla entities i listan
            {
                try //Försöker lägga till entity, om det inte går ignorera och skriv ut i consolen
                {
                    entitiesInScene.Add(entity);
                    System.Console.WriteLine(entity.name + " has been added");
                }
                catch
                {
                    System.Console.WriteLine(entity.name + " do not exist");
                }
            }
            entitiesToAdd.Clear(); //rensar listan 
        }
    }
}