export default function callApi(data){
    return new Promise((resolve,reject)=>{
        const url = 'http://localhost:3001/items/'+data.id;
        fetch(url,{
            method: 'PUT',
            headers: {"Content-Type" : "Application/json"},
            body: JSON.stringify({name:data.name})
        })
            .then((response)=>response.json())
            .then((res)=>{
                resolve(res);
                console.log(res);
            })
            .catch((error)=>{
                reject(error);
            });
    });
}