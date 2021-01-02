export default function callApi(data){
    return new Promise((resolve,reject)=>{
        const url = 'http://localhost:3001/items';
        fetch(url,{
            method: 'POST',
            headers: {"Content-Type" : "Application/json"},
            body: JSON.stringify(data)
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