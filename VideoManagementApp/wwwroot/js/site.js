function showCatalogue() {
    document.getElementById('catalogue-section').style.display = 'block';
    document.getElementById('upload-section').style.display = 'none';
    document.querySelectorAll('.nav-link').forEach(link => link.classList.remove('active'));
    document.querySelector('.nav-link[onclick="showCatalogue()"]').classList.add('active');
}

function showUpload() {
    document.getElementById('catalogue-section').style.display = 'none';
    document.getElementById('upload-section').style.display = 'block';
    document.querySelectorAll('.nav-link').forEach(link => link.classList.remove('active'));
    document.querySelector('.nav-link[onclick="showUpload()"]').classList.add('active');
}

async function uploadFiles() {   

    const files = document.getElementById('files').files;
    const formData = new FormData();
    for (let i = 0; i < files.length; i++) {
        formData.append('files', files[i]);
    }

    try {
        const response = await fetch('/api/upload', {
            method: 'POST',
            body: formData
        });

        if (response.ok) {            
            location.reload();
        } else {                   
            const jsonResponse = await response.json();

            let errors = [];

            if (jsonResponse.errors) {
                for (let key in jsonResponse.errors) {
                    if (jsonResponse.errors.hasOwnProperty(key)) {
                        errors = errors.concat(jsonResponse.errors[key]);
                    }
                }
            }
            else errors = errors.concat(jsonResponse.error);

            displayError(errors, response.status);
        }
    } catch (error) {
        displayError(error, null);
    }
}

function displayError(message, status) {
    const errorBox = document.getElementById('errorBox');
    let errorMessage = 'An error has occurred while uploading file(s).';

    if (status) {
        errorMessage += ` Response Code ${status}. Error Message: ${message} Please try again.`;        
    } 

    errorBox.textContent = errorMessage;
    errorBox.style.display = 'block';
}

function playVideo(fileName) {
    const videoPlayer = document.getElementById('videoPlayer');
    videoPlayer.src = `/media/${fileName}`;
    videoPlayer.load();
    videoPlayer.play();
}

// Initialize with catalogue view
showCatalogue();
