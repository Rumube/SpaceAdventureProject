const WebSocket = require('ws')

const server = new WebSocket.Server({ port: 8080 }, servidorIniciado)

let _numUsuarios = 0
let _listaClientes = []

function servidorIniciado() {
    console.log("Sevidor iniciado")
}

server.on('connection', conexionRealizada)

function conexionRealizada(cliente) {
    _numUsuarios++;
    _listaClientes.push(cliente)
    console.log("Se conecto el cliente: " + cliente + " " + _listaClientes.length)
    cliente.on('close', () => {
        _numUsuarios--
        let i = _listaClientes.indexOf(cliente)
        _listaClientes.splice(i, 1);
        console.log('Usuario desconectado ' + _listaClientes.length)
    })
    cliente.on("message", (data) => {
        console.log(data.toString());
        //cliente.send("Recivido")
        for (i = 0; i < _listaClientes.length; i++) {
            _listaClientes[i].send(data.toString());
        }
    })
}

