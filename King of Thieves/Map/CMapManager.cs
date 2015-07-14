using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gears.Cartography;
using Gears.Playable;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Map
{
    class CMapManager
    {
        private static CMap _currentMap;
        private Dictionary<string, CMap> mapPool = null;
        private static Actors.CComponent _droppableComponent = new Actors.CComponent(1);
        private static Dictionary<string, Graphics.CSprite> _droppableActorSpriteCache = new Dictionary<string, Graphics.CSprite>();
        private static bool _roomStart = false;


        public static object propertyGetter(string actorName, Map.EActorProperties property)
        {
            return _currentMap.getProperty(actorName, property);
        }

        public static object propertyGetterFromComponent(int componentAddress, string actorName, Map.EActorProperties property)
        {
            return _currentMap.getProperty(componentAddress, actorName, property);
        }

        public CMapManager()
        {
            _currentMap = null;
            mapPool = new Dictionary<string, CMap>();

            //cache droppable sprites here
        }

        ~CMapManager()
        {
            clear();
        }

        public Actors.CActor setActorToFollow(string actorName)
        {
            CMasterControl.camera.actorToFollow = _currentMap.queryActorRegistry(actorName)[0];
            return CMasterControl.camera.actorToFollow;
        }

        public Graphics.CSprite getDroppableSprite(string droppable)
        {
            try
            {
                return _droppableActorSpriteCache[droppable];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public static bool roomStart
        {
            get
            {
                return _roomStart;
            }
        }

        public static void turnOffRoomStart()
        {
            _roomStart = false;
        }

        public void drawMap()
        {
            if (_currentMap != null)
                _currentMap.draw();
        }

        public void updateMap(GameTime gameTime)
        {
            if (_currentMap != null)
                _currentMap.update(gameTime);
        }

        public static Actors.CActor[] queryActorRegistry(Type type, int layer)
        {
            try
            {
                return _currentMap.queryActorRegistry(type, layer);
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        public void swapMap(string mapName, string actorToFollow, Vector2 followerCoords)
        {
            _currentMap = mapPool[mapName];
            CMasterControl.commNet.Clear();
            _currentMap.registerWithCommNet();

            
            Actors.CActor actor = setActorToFollow(actorToFollow);
            Vector3 cameraDiff = new Vector3(-CMasterControl.camera.position.X - followerCoords.X, -CMasterControl.camera.position.Y - followerCoords.Y, 0);

            CMasterControl.camera.translate(cameraDiff);
            CMasterControl.camera.translate(new Vector3(160, 120, 0));
            CMasterControl.camera.setBoundary(new Vector2(followerCoords.X - 80,followerCoords.Y - 60));
            actor.position = followerCoords;

            _roomStart = true;
        }

        public void cacheMaps(bool clearMaps, params string[] maps)
        {
            if (clearMaps)
                clear();

            foreach (string file in maps)
                mapPool.Add(file, new CMap(file)); //temporary
        }

        private void clear()
        {
            if (mapPool != null)
                mapPool.Clear();

            mapPool = null;
        }


        public static void removeFromActorRegistry(Actors.CActor actor)
        {
            _currentMap.removeFromActorRegistry(actor);
        }

        public static void removeComponent(Actors.CComponent component)
        {
            _currentMap.removeComponent(component, component.layer);
        }

        public static void addActorToComponent(Actors.CActor actor, int componentAddress)
        {
            _currentMap.addActorToComponent(actor, componentAddress);
        }
    }
}
