let fs = require('fs')
let entities = {}

function objectSet(player, obj) {
  obj = JSON.parse(obj)
  entities[obj.id] = obj
}

function toFixed(obj) {
  obj.x = parseFloat(obj.x.toFixed(4))
  obj.y = parseFloat(obj.y.toFixed(4))
  obj.z = parseFloat(obj.z.toFixed(4))
}

function saveMap(file, name, author, gamemode, desc) {
  let obj = {
    meta: {
      name, author, gamemode,
      description: desc
    }
  }

  // escape " in string, then replace with \" at line 42:54
  for (let key in obj.meta)
    obj.meta[key] = obj.meta[key].replace(/"/g, '--"')

  for (let key in entities) {
    let ent = entities[key]
    if (!obj[ent.type+'s'])
      obj[ent.type+'s'] = []
    let _ent = {}
    _ent.model = ent.model
    toFixed(ent.position)
    _ent.pos = ent.position
    if (ent.type != 'marker') {
      toFixed(ent.rotation)
      _ent.rot = ent.rotation
    }
    obj[ent.type+'s'].push('/'+JSON.stringify(_ent)+'/')
  }

  let data = JSON.stringify(obj, null, 1)
  data = data.replace(/(s*"\/)|\/"|\\/g, '').replace(/--"/g, '\\"')
  fs.writeFileSync(`./maps/${file}.json`, data)
}

mp.events.add({
  'me:upsertEntity': objectSet,

  'me:deleteObject': (player, id)=> {
    delete entities[id]
  },

  'me:checkFileExists': (player, file)=> {
    let exist = fs.existsSync('./maps/'+file+'.json')
    player.call('me:checkFileResult', [exist])
  },

  'me:saveMap': (player, file, name, author, gamemode, desc)=> {
    if (!name)
      name = 'Unnamed'
    if (!author)
      name = 'Unkown'
    saveMap(file, name, author, gamemode, desc)
    player.notify('~b~Map saved')
  },

  'me:newMap': ()=> {
    entities = {}
  },

  'me:getMaps': player=> {
    let maps = fs.readdirSync('./maps')
    maps = maps.filter(file=> file.endsWith('.json'))
    maps = maps.map(file=> file.replace('.json', ''))
    player.call('me:gotMaps', [maps])
  },

  'me:openMap': (player, map)=> {
    // send the file as stream of chunks
    let file = fs.readFileSync('./maps/'+map+'.json', 'utf-8')
    file = file.replace(/\n/g, '')
    let buffer = file.match(/.{1,7500}/g)
    buffer.forEach((chunk,i) => {
      let eof = null
      if (i == buffer.length - 1)
        eof = true
      player.call('me:streamMapChunks', [chunk, eof])
    })
  }
})