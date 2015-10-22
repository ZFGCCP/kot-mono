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
        private int _width = 0;
        private int _height = 0;
        public int hitBoxCounter = 0;

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

        public void swapDrawDepth(int newDepth, Actors.CActor sprite)
        {
            _layers[sprite.layer].swapDrawDepth(newDepth, sprite);
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

        public void switchComponentToLayer(CComponent component, int toLayer)
        {
            //draw list swap
            _layers[component.layer].removeFromDrawList(component.root);
            _layers[toLayer].addToDrawList(component.root);
            component.root.layer = toLayer;
            foreach (CActor actor in component.actors.Values)
            {
                _layers[component.layer].removeFromDrawList(actor);
                _layers[toLayer].addToDrawList(actor);
                actor.layer = toLayer;
            }

            int fromLayer = component.layer;
            _layers[fromLayer].removeComponent(component);
            component.layer = toLayer;
            _layers[toLayer].addComponent(component);
        }

        public CMap(string fileName, Dictionary<string, Graphics.CSprite> atlasCache = null)
        {
            _internalMap = Gears.Cartography.Map.deserialize(fileName);
            _layers = new List<CLayer>(_internalMap.NUM_LAYERS);
            int layerCount = 0;

            /*if (_internalMap.TILESET != null)
                _tileIndex = new Graphics.CSprite(_internalMap.TILESET, Graphics.CTextures.textures[_internalMap.TILESET]);*/

            _width = _internalMap.WIDTH;
            _height = _internalMap.HEIGHT;
            _largestAddress = 0;
            foreach (Gears.Cartography.layer layer in _internalMap.LAYERS)
            {
                
                int componentAddresses = 2;
                int componentCount = 0;
                int hitboxAddress = CReservedAddresses.HITBOX_NOT_PRESENT;

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
                    Vector2 atlasCoordsEnd = Vector2.Zero;
                    if (tile.TILESELECTIONEND != null)
                        atlasCoordsEnd = new Vector2((float)Convert.ToDouble(_valSplitter.Split(tile.TILESELECTIONEND)[0]),
                                                             (float)Convert.ToDouble(_valSplitter.Split(tile.TILESELECTIONEND)[1]));

                    if (!tileSets.ContainsKey(tile.TILESET))
                        tileSets.Add(tile.TILESET, new Graphics.CSprite(tile.TILESET));

                    if (tile.TILESELECTIONEND == null)
                        tiles[tileCounter++] = new CTile(atlasCoords, mapCoords, tile.TILESET);
                    else
                        tiles[tileCounter++] = new CAnimatedTile(atlasCoords, atlasCoordsEnd, mapCoords, tile.TILESET, tile.SPEED);


                }

                List<CActor> actorsForDrawList = new List<CActor>();
                if (layer.COMPONENTS != null)
                {
                    //=======================================================================
                    //Components
                    //=======================================================================
                    
                    foreach (Gears.Cartography.component component in layer.COMPONENTS)
                    {
                        CComponent tempComp = new CComponent(component.ADDRESS);
                        foreach (Gears.Cartography.actors actor in component.ACTORS)
                        {
                            Type actorType = Type.GetType(actor.TYPE);

                            if (actorType == typeof(King_of_Thieves.Actors.Collision.CSolidTile))
                            {
                                hitboxAddress = component.ADDRESS;
                                hitBoxCounter += 1;
                            }

                            CActor tempActor = (CActor)Activator.CreateInstance(actorType);

                            Vector2 coordinates = Vector2.Zero;

                            if (!_coordFormat.IsMatch(actor.COORDS))
                                throw new FormatException("The coordinate format provided was not valid.\n" + "Actor: " + actor.TYPE.ToString() + " " + actor.NAME);

                            coordinates.X = (float)Convert.ToDouble(_valSplitter.Split(actor.COORDS)[0]);
                            coordinates.Y = (float)Convert.ToDouble(_valSplitter.Split(actor.COORDS)[1]);

                            tempComp.addActor(tempActor, actor.NAME);

                            tempActor.init(actor.NAME, coordinates, actorType.ToString(), componentAddresses, actor.param == null ? null : actor.param.Split(','));
                            tempActor.layer = layerCount;
                            tempActor.swapImage(CActor._MAP_ICON);
                            _actorRegistry.Add(tempActor);
                            actorsForDrawList.Add(tempActor);

                        }
                        //register component
                        _componentRegistry.Add(tempComp);
                        tempComp.layer = layerCount;
                        if (tempComp.address > _largestAddress)
                            _largestAddress = tempComp.address;

                        compList[componentCount++] = tempComp;
                        componentAddresses++;

                    }
                }
                CLayer tempLayer = new CLayer(layer.NAME, compList, tiles, ref _tileIndex, layerCount, Convert.ToDouble(_internalMap.VERSION),hitboxAddress);
                tempLayer.addToDrawList(actorsForDrawList);
                actorsForDrawList.Clear();
                _layers.Add(tempLayer);
                layerCount++;
                //_layers[layerCount] = new CLayer(layer.NAME, compList, tiles, ref _tileIndex, Convert.ToDouble(_internalMap.VERSION));

                if (atlasCache == null)
                    tempLayer.otherImages = tileSets;
                else
                    tempLayer.otherImages = atlasCache;

            }

            /*Actors.CComponent[] managers = _createManagers();
            //add controllers
            foreach (Actors.CComponent component in managers)
            {
                _layers[0].addComponent(component);
                _componentRegistry.Add(component);
            }*/

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
                    _internalMap.LAYERS[i].COMPONENTS = new Gears.Cartography.component[_layers[i].componentCount];

                    int componentCounter = 0;
                    foreach (CComponent component in _componentRegistry)
                    {
                        if (component.layer == i)
                        {
                            _internalMap.LAYERS[i].COMPONENTS[componentCounter] = new Gears.Cartography.component();
                            Gears.Cartography.component comp = _internalMap.LAYERS[i].COMPONENTS[componentCounter];
                            comp.ADDRESS = component.address;

                            comp.ACTORS = new Gears.Cartography.actors[component.actors.Count + 1];

                            //write the root component
                            comp.ACTORS[0] = new Gears.Cartography.actors();
                            comp.ACTORS[0].NAME = component.root.name;
                            comp.ACTORS[0].TYPE = component.root.dataType;
                            comp.ACTORS[0].COORDS = component.root.position.X + ":" + component.root.position.Y;

                            foreach (string param in component.root.mapParams)
                                comp.ACTORS[0].param += param + ",";

                            if (comp.ACTORS[0].param != null)
                                comp.ACTORS[0].param = comp.ACTORS[0].param.Substring(0, comp.ACTORS[0].param.Length - 1);

                            //write the rest
                            for (int j = 1; j < comp.ACTORS.Count(); j++)
                            {
                                List<CActor> actors = component.actors.Values.ToList();

                                comp.ACTORS[j] = new Gears.Cartography.actors();
                                comp.ACTORS[j].NAME = actors[j - 1].name;
                                comp.ACTORS[j].TYPE = actors[j - 1].dataType;
                                comp.ACTORS[j].COORDS = actors[j - 1].position.X + ":" + actors[j - 1].position.Y;

                                foreach (string param in actors[j - 1].mapParams)
                                    comp.ACTORS[j].param += param + ",";

                                if (comp.ACTORS[j].param != null)
                                    comp.ACTORS[j].param = comp.ACTORS[j].param.Substring(0, comp.ACTORS[j].param.Length - 1);
                            }
                            componentCounter++;
                        }
                    }

                    for (int j = 0; j < _internalMap.LAYERS[i].TILES.Count(); j++)
                    {
                        _internalMap.LAYERS[i].TILES[j] = new Gears.Cartography.tile();

                        CTile temp = _layers[i].getTileInfo(j);
                        _internalMap.LAYERS[i].TILES[j].COORDS = temp.tileCoords.X + ":" + temp.tileCoords.Y;
                        _internalMap.LAYERS[i].TILES[j].TILESELECTION = temp.atlasCoords.X + ":" + temp.atlasCoords.Y;
                        _internalMap.LAYERS[i].TILES[j].TILESET = temp.tileSet;

                        CAnimatedTile animTemp = temp as CAnimatedTile;
                        if (animTemp != null)
                        {
                            _internalMap.LAYERS[i].TILES[j].TILESELECTION = animTemp.startingPosition.X + ":" + animTemp.startingPosition.Y;
                            _internalMap.LAYERS[i].TILES[j].TILESELECTIONEND = animTemp.atlasCoordsEnd.X + ":" + animTemp.atlasCoordsEnd.Y;
                            _internalMap.LAYERS[i].TILES[j].SPEED = animTemp.speed;
                        }

                        temp = null;
                        animTemp = null;

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

            if (component.root != null)
                _layers[layer].addToDrawList(component.root);

            foreach(KeyValuePair<string,CActor> kvp in component.actors)
                _layers[layer].addToDrawList(kvp.Value);

            if (component.address > _largestAddress)
                _largestAddress = component.address;
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
                    removeComponent(_componentRegistry[i], _componentRegistry[i].layer);

            foreach (CLayer layer in _layers)
                layer.updateLayer(gameTime);

            //handle collisions
            for (int i = 0; i < _componentRegistry.Count(); i++)
                _componentRegistry[i].doCollision();

            CMasterControl.mapManager.checkAndSwapMap();
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
