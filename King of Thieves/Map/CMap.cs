using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using King_of_Thieves.Actors;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Map
{
    class CMap
    {
        public List<CLayer> _layers = null;
        private static Regex _coordFormat = new Regex("^[0-9]+:[0-9]+$");
        private static Regex _valSplitter = new Regex(":");
        private List<CActor> _actorRegistry = new List<CActor>();
        private List<CComponent> _componentRegistry = new List<CComponent>(); //todo: make this a hashmap
        private int _largestAddress = 0;
        private Gears.Cartography.Map _internalMap;
        private Graphics.CSprite _tileIndex = null;

        public CMap(Dictionary<string, Graphics.CSprite> atlasCache = null)
        {
            _internalMap = null;
            _layers = new List<CLayer>(1);
            _layers.Add(new CLayer(atlasCache));
            _layers[0].NAME = "root";
        }

        public CMap(params CLayer[] layers)
        {
            _layers = layers.ToList();
        }

        private Actors.CComponent[] _createManagers()
        {
            Actors.CComponent[] managers = new Actors.CComponent[1];

            //drop manager
            Actors.CActor dropManagerRoot = new Actors.Controllers.GameControllers.CDropController();
            managers[0] = new Actors.CComponent(Actors.CReservedAddresses.DROP_CONTROLLER);
            managers[0].addActor(dropManagerRoot, "dropManagerRoot");

            return managers;
        }

        public CMap(string fileName)
        {
            _internalMap = Gears.Cartography.Map.deserialize(fileName);
            _layers = new List<CLayer>(_internalMap.NUM_LAYERS);
            int layerCount = 0;

            /*if (_internalMap.TILESET != null)
                _tileIndex = new Graphics.CSprite(_internalMap.TILESET, Graphics.CTextures.textures[_internalMap.TILESET]);*/
           


            foreach (Gears.Cartography.layer layer in _internalMap.LAYERS)
            {
                
                int componentAddresses = 2;
                int componentCount = 0;
                Actors.CComponent[] compList = new CComponent[layer.COMPONENTS == null ? 0 : layer.COMPONENTS.Count()];
                Dictionary<string, Graphics.CSprite> tileSets = new Dictionary<string, Graphics.CSprite>();
                //=======================================================================
                //Tiles
                //=======================================================================
                CTile[] tiles = new CTile[layer.TILES.Count()];
                int tileCounter = 0;
                foreach (Gears.Cartography.tile tile in layer.TILES)
                {
                    if (!_coordFormat.IsMatch(tile.COORDS))
                        throw new FormatException("The coordinate format provided was not valid.\n" + "Tile: " + tile.COORDS);

                    if (!_coordFormat.IsMatch(tile.TILESELECTION))
                        throw new FormatException("The coordinate format provided was not valid.\n" + "Tile: " + tile.TILESELECTION);

                    Vector2 atlasCoords = new Vector2((float)Convert.ToDouble(_valSplitter.Split(tile.TILESELECTION)[0]),
                                                      (float)Convert.ToDouble(_valSplitter.Split(tile.TILESELECTION)[1]));
                    Vector2 mapCoords = new Vector2((float)Convert.ToDouble(_valSplitter.Split(tile.COORDS)[0]),
                                                      (float)Convert.ToDouble(_valSplitter.Split(tile.COORDS)[1]));

                    if (!tileSets.ContainsKey(tile.TILESET))
                        tileSets.Add(tile.TILESET, new Graphics.CSprite(tile.TILESET));

                    tiles[tileCounter++] = new CTile(atlasCoords, mapCoords, tile.TILESET);


                }

                List<CActor> actorsForDrawList = new List<CActor>();
                if (layer.COMPONENTS != null)
                {
                    //=======================================================================
                    //Components
                    //=======================================================================
                    
                    foreach (Gears.Cartography.component component in layer.COMPONENTS)
                    {
                        CComponent tempComp = new CComponent(componentAddresses);
                        foreach (Gears.Cartography.actors actor in component.ACTORS)
                        {
                            Type actorType = Type.GetType(actor.TYPE);
                            
                            CActor tempActor = (CActor)Activator.CreateInstance(actorType);

                            Vector2 coordinates = Vector2.Zero;

                            if (!_coordFormat.IsMatch(actor.COORDS))
                                throw new FormatException("The coordinate format provided was not valid.\n" + "Actor: " + actor.TYPE.ToString() + " " + actor.NAME);

                            coordinates.X = (float)Convert.ToDouble(_valSplitter.Split(actor.COORDS)[0]);
                            coordinates.Y = (float)Convert.ToDouble(_valSplitter.Split(actor.COORDS)[1]);

                            tempComp.addActor(tempActor, actor.NAME);

                            tempActor.init(actor.NAME, coordinates, actorType.ToString(), componentAddresses, actor.param == null ? null : actor.param.Split(','));
                            tempActor.layer = layerCount;

                            _actorRegistry.Add(tempActor);
                            actorsForDrawList.Add(tempActor);

                        }
                        //register component
                        _componentRegistry.Add(tempComp);
                        _largestAddress = componentAddresses;
                        compList[componentCount++] = tempComp;
                        componentAddresses++;

                    }
                }
                CLayer tempLayer = new CLayer(layer.NAME, compList, tiles, ref _tileIndex, layerCount, Convert.ToDouble(_internalMap.VERSION));
                tempLayer.addToDrawList(actorsForDrawList);
                actorsForDrawList.Clear();
                _layers.Add(tempLayer);
                //_layers[layerCount] = new CLayer(layer.NAME, compList, tiles, ref _tileIndex, Convert.ToDouble(_internalMap.VERSION));
                _layers[layerCount++].otherImages = tileSets;

            }

            Actors.CComponent[] managers = _createManagers();
            //add controllers
            foreach (Actors.CComponent component in managers)
            {
                _layers[0].addComponent(component);
                _componentRegistry.Add(component);
            }

        }

        public CLayer getLayer(int index)
        {
            return _layers[index];
        }

        public object getProperty(int componentAddress, string actorName, Map.EActorProperties property)
        {
            object propertyVal = null;

            foreach (CComponent component in _componentRegistry)
            {
                if (component.getAddress() == componentAddress)
                    if ((propertyVal = component.getProperty(actorName, property)) != null)
                        return propertyVal;
                    else
                        return null;
            }

            return propertyVal;
        }

        public object getProperty(string actorName, Map.EActorProperties property)
        {
            object propertyVal = null;

            foreach (CComponent component in _componentRegistry)
            {
                if ((propertyVal = component.getProperty(actorName, property)) != null)
                {
                    return propertyVal;
                }
            }

            return propertyVal;
        }

        public void writeMap(string fileName)
        {
            //Gears.Cartography.Map
            if (_internalMap == null)
            {
                _internalMap = new Gears.Cartography.Map();

                _internalMap.VERSION = "1";
                _internalMap.LAYERS = new Gears.Cartography.layer[_layers.Count()];
                _internalMap.NUM_LAYERS = (byte)_internalMap.LAYERS.Count();
                _internalMap.TILESET = _tileIndex == null ? null : _tileIndex.atlasName;

                for (int i = 0; i < _layers.Count(); i++)
                {
                    Dictionary<int, List<string>> actorData = null;
                    _internalMap.LAYERS[i] = new Gears.Cartography.layer();
                    _internalMap.LAYERS[i].LAYER_WIDTH = _layers[i].width;
                    _internalMap.LAYERS[i].LAYER_HEIGHT = _layers[i].height;
                    _internalMap.LAYERS[i].NAME = _layers[i].NAME;

                    _internalMap.LAYERS[i].TILES = new Gears.Cartography.tile[_layers[i].numberOfTiles];
                    actorData = _layers[i].getActorHeaderInfo();
                    //_internalMap.LAYERS[i].COMPONENTS = new Gears.Cartography.component[actorData.Keys.Count];

                    /*int componentCounter = 0;
                    foreach (KeyValuePair<int, List<String>> kvp in actorData)
                    {
                        _internalMap.LAYERS[i].COMPONENTS[componentCounter] = new Gears.Cartography.component();
                        Gears.Cartography.component comp = _internalMap.LAYERS[i].COMPONENTS[componentCounter];
                        comp.ADDRESS = kvp.Key;

                        comp.ACTORS = new Gears.Cartography.actors[kvp.Value.Count()];

                        for (int j = 0; j < kvp.Value.Count(); j++)
                        {
                            string[] header = kvp.Value[j].Split(';');
                            comp.ACTORS[j].NAME = header[0];
                            comp.ACTORS[j].TYPE = header[1];
                            comp.ACTORS[j].COORDS = header[2];
                        }
                        componentCounter++;
                    }*/

                    for (int j = 0; j < _internalMap.LAYERS[i].TILES.Count(); j++)
                    {
                        _internalMap.LAYERS[i].TILES[j] = new Gears.Cartography.tile();

                        CTile temp = _layers[i].getTileInfo(j);
                        _internalMap.LAYERS[i].TILES[j].COORDS = temp.tileCoords.X + ":" + temp.tileCoords.Y;
                        _internalMap.LAYERS[i].TILES[j].TILESELECTION = temp.atlasCoords.X + ":" + temp.atlasCoords.Y;
                        _internalMap.LAYERS[i].TILES[j].TILESET = temp.tileSet;
                        temp = null;

                    }
                    
                }
            }

            _internalMap.serializeToXml(fileName);

        }

        public void addDrop(Actors.Items.Drops.CDroppable drop, int layer)
        {
            //index 1 will always be guaranteed to be the droppable component
            _componentRegistry[1].actors.Add("drop" + _componentRegistry[1].actors.Count, drop);
            _actorRegistry.Add(drop);
        }

        public void addComponent(CComponent component, int layer)
        {
            _layers[layer].addComponent(component);
            _componentRegistry.Add(component);
            _largestAddress = component.address;
            CMasterControl.commNet.Add(component.address, new List<CActorPacket>());

            _layers[layer].addToDrawList(component.root);

            foreach(KeyValuePair<string,CActor> kvp in component.actors)
                _layers[layer].addToDrawList(kvp.Value);
        }

        public void removeComponent(CComponent component, int layer)
        {
            _layers[layer].removeComponent(component);
            _componentRegistry.Remove(component);
            CMasterControl.commNet.Remove(component.address);
        }

        public void draw(SpriteBatch spriteBatch = null)
        {
            foreach (CLayer layer in _layers)
            {
                layer.drawLayer(spriteBatch);
            }
        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < _componentRegistry.Count(); i++)
                if (_componentRegistry[i].killMe)
                {
                    removeComponent(_componentRegistry[i], _componentRegistry[i].layer);
                    _componentRegistry.Remove(_componentRegistry[i]);
                }

            foreach (CLayer layer in _layers)
                layer.updateLayer(gameTime);

            //handle collisions
            for (int i = 0; i < _componentRegistry.Count(); i++)
                _componentRegistry[i].doCollision();
        }

        public CComponent queryComponentRegistry(int id)
        {
            var query = from component in _componentRegistry
                        where component.getAddress() == id
                        select component;
            CComponent[] result = query.ToArray();

            if (result.Length == 0)
                return null;

            return query.ToArray()[0];
        }

        private bool _isTypePartOfFamily(Type family, Type checkMe)
        {
            Type iterator = checkMe;
            while (iterator != typeof(Actors.CActor))
            {
                if (family == iterator)
                    return true;
                else
                    iterator = iterator.BaseType;
            }
            return false;
        }

        public Actors.CActor[] queryActorRegistry(Type type, int layer)
        {
            var query = from actor in _actorRegistry
                        where _isTypePartOfFamily(type,actor.GetType()) && actor.layer == layer
                        select actor;


            return query.ToArray();
        }

        public Actors.CActor[] queryActorRegistry(String actorName)
        {
            var query = from actor in _actorRegistry
                        where actor.name == actorName
                        select actor;

            return query.ToArray();
        }

        private void serializeToXml(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Gears.Cartography.Map));
            TextWriter writer = new StreamWriter(fileName);

            serializer.Serialize(writer, this);
            writer.Close();
        }

        public void removeFromActorRegistry(CActor actor)
        {
            _actorRegistry.Remove(actor);
            _layers[actor.layer].removeFromDrawList(actor);
        }

        public void registerWithCommNet()
        {
            foreach (CComponent comp in _componentRegistry)
                CMasterControl.commNet.Add(comp.address, new List<CActorPacket>());
        }

        public void addToActorRegistry(CActor actor)
        {
            _actorRegistry.Add(actor);
        }

        public void addActorToComponent(CActor actor, int componentId)
        {
            queryComponentRegistry(componentId).addActor(actor, actor.name);
            addToActorRegistry(actor);

            _layers[actor.layer].addToDrawList(actor);
        }

        public void removeActorFromComponent(CActor actor, int componentId)
        {
            removeFromActorRegistry(actor);
        }

        public int largestAddress
        {
            get
            {
                return _largestAddress;
            }
        }
    }
}
